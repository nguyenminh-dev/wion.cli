using Wion.Cli.Services;

namespace Wion.Cli.Commands;

public static class NewCommand
{
    public static void Configure(Command rootCommand)
    {
        var newCommand = rootCommand.CreateCommand("new");
        newCommand.Description = "Create a new project from template";

        var projectNameArg = new Argument<string>("projectName", "The name of the project to create");
        var verboseOption = new Option<bool>("--verbose", "Enable verbose logging");

        newCommand.AddArgument(projectNameArg);
        newCommand.AddOption(verboseOption);

        newCommand.SetHandler(async (string projectName, bool verbose) =>
        {
            var templatePath = ResolveTemplatePath();
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), projectName);

            if (!Directory.Exists(templatePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Template directory not found at: {templatePath}");
                Console.ResetColor();
                return;
            }

            try
            {
                var processor = new TemplateProcessor(templatePath, outputPath, verbose);
                await processor.ProcessAsync(projectName);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✓ Project '{projectName}' created successfully!");
                Console.WriteLine($"  Location: {outputPath}");
                Console.WriteLine($"\nNext steps:");
                Console.WriteLine($"  cd {projectName}");
                Console.WriteLine($"  dotnet restore");
                Console.WriteLine($"  dotnet build");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Error: {ex.Message}");
                if (verbose)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                Console.ResetColor();
            }
        }, projectNameArg, verboseOption);

        rootCommand.AddCommand(newCommand);
    }

    private static string ResolveTemplatePath()
    {
        // Try multiple paths to find the template directory
        var possiblePaths = new[]
        {
            // When running from published output
            Path.Combine(AppContext.BaseDirectory, "Wion.Template"),
            // When running from project directory (development)
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Wion.Template"),
            // When running from solution directory
            Path.Combine(Directory.GetCurrentDirectory(), "Wion.Template"),
            // When CLI is in bin/Debug or bin/Release
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Wion.Template"),
            // Relative to the executable
            Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "", "..", "Wion.Template")
        };

        foreach (var path in possiblePaths)
        {
            var resolvedPath = Path.GetFullPath(path);
            if (Directory.Exists(resolvedPath))
            {
                return resolvedPath;
            }
        }

        // Default to relative path
        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Wion.Template"));
    }
}