using Wion.Template.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Wion.Template.Permissions;

public class TemplatePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TemplatePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(TemplatePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TemplateResource>(name);
    }
}
