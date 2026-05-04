using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Wion.Test.EntityFrameworkCore;
using Wion.Test.MultiTenancy;

namespace Wion.Test;

[DependsOn(
    typeof(TemplateHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(TemplateApplicationModule),
    typeof(TemplateEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule)
    )]
public class TemplateHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        ConfigureConventionalControllers();
        ConfigureSwagger(context, configuration);
        ConfigureVirtualFileSystem(context);
        ConfigureCors(context, configuration);
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<TemplateDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wion.Test.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<TemplateDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wion.Test.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<TemplateApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wion.Test.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<TemplateApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wion.Test.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(TemplateApplicationModule).Assembly);
        });
    }

    private static void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddOpenApiDocument(document =>
        {
            document.Title = TemplateConsts.ApiDocTitle;
            document.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the bearer scheme",
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Type = OpenApiSecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                Flow = OpenApiOAuth2Flow.Implicit,
            });
            document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));

            var descriptions = new Dictionary<string, string>
            {
                { "200", "OK" },
                { "201", "Created" },
                { "202", "Accepted" },
                { "400", "Bad Request" },
                { "401", "Unauthorized" },
                { "403", "Forbidden" },
                { "404", "Not Found" },
                { "405", "Mehtod Not Allowed" },
                { "406", "Not Acceptable" },
                { "500", "Server Error" }
            };

            document.PostProcess = document =>
                            document.Paths.Values
                                .SelectMany(p => p.Values)
                                .SelectMany(p => p.Responses)
                                .Where(r => string.IsNullOrWhiteSpace(r.Value.Description))
                                .ToList()
                                .ForEach(res =>
                                {
                                    if (descriptions.ContainsKey(res.Key))
                                        res.Value.Description = descriptions[res.Key];
                                });
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.Trim().RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        app.UseForwardedHeaders();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        app.UseRouting();
        app.UseAbpSecurityHeaders();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();

        app.UseOpenApi();
        app.UseSwaggerUi(i =>
        {
            i.DocumentTitle = TemplateConsts.ApiDocTitle;
        });
        app.UseReDoc(c =>
        {
            c.Path = "/redoc";
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
