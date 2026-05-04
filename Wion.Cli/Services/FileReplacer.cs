using System.Text;
using System.Text.RegularExpressions;

namespace Wion.Cli.Services;

public class FileReplacer
{
    private readonly ILogger _logger;

    public FileReplacer()
    {
        _logger = new Logger();
    }

    public async Task ReplaceContentAsync(string filePath, string templateName, string newProjectName)
    {
        try
        {
            var content = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            var newContent = content;

            var replacements = GetReplacementStrings(templateName, newProjectName);

            foreach (var replacement in replacements)
            {
                if (newContent.Contains(replacement.From))
                {
                    newContent = newContent.Replace(replacement.From, replacement.To);
                    _logger.LogDebug($"Replaced in {Path.GetFileName(filePath)}: {replacement.From} -> {replacement.To}");
                }
            }

            if (content != newContent)
            {
                await File.WriteAllTextAsync(filePath, newContent, Encoding.UTF8);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing file {filePath}: {ex.Message}");
            throw;
        }
    }

    private List<Replacement> GetReplacementStrings(string templateName, string newProjectName)
    {
        var templateShortName = "Template";
        // Extract short name by removing "Wion." prefix and all remaining dots
        // Example: "Wion.TestProject.ProjectA" -> "TestProjectProjectA"
        var newProjectShortName = newProjectName.StartsWith("Wion.")
            ? newProjectName.Substring(5).Replace(".", "") // Remove "Wion." and all dots
            : newProjectName.Replace(".", ""); // Remove all dots if doesn't start with "Wion."

        return new List<Replacement>
        {
            // Full name replacements (e.g., "Wion.Template" -> "Wion.Test")
            new Replacement(templateName, newProjectName),
            // Short name replacements (e.g., "Template" -> "Test", "TemplateDomainModule" -> "TestDomainModule")
            new Replacement(templateShortName, newProjectShortName),
            // File extensions
            new Replacement($".{templateName.ToLower()}", $".{newProjectName.ToLower()}"),
            // Solution file
            new Replacement($"{templateName}.sln", $"{newProjectName}.sln"),
            // Common.props reference
            new Replacement($"$(MSBuildProjectDirectory)/../{templateName}.sln", $"$(MSBuildProjectDirectory)/../{newProjectName}.sln"),
            // XML namespace replacements
            new Replacement($"xmlns:{templateShortName}", $"xmlns:{newProjectShortName}"),
            new Replacement($"xmlns:ns={templateShortName}", $"xmlns:ns={newProjectShortName}"),
            // Class name variations
            new Replacement($"public partial class {templateShortName}Configuration", $"public partial class {newProjectShortName}Configuration"),
            // Method names
            new Replacement($"{templateShortName}Consts", $"{newProjectShortName}Consts"),
            // Constant replacements
            new Replacement($@"{templateShortName}Consts""", $@"{newProjectShortName}Consts"""),
            // Namespace variations
            new Replacement($"namespace {templateName}.Templates", $"namespace {newProjectName}.Templates"),
            // Interface names (short form)
            new Replacement($"I{templateShortName}", $"I{newProjectShortName}"),
            // Interface names (full form)
            new Replacement($"I{templateName}", $"I{newProjectName}"),
        };
    }

    private class Replacement
    {
        public string From { get; }
        public string To { get; }

        public Replacement(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}