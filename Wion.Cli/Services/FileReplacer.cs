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
        return new List<Replacement>
        {
            new Replacement(templateName, newProjectName),
            // File extensions
            new Replacement($".{templateName.ToLower()}", $".{newProjectName.ToLower()}"),
            // ABP module definition files
            new Replacement($"{templateName}.abpmdl", $"{newProjectName}.abpmdl"),
            new Replacement($"{templateName}.abpsln", $"{newProjectName}.abpsln"),
            // Solution file
            new Replacement($"{templateName}.sln", $"{newProjectName}.sln"),
            // Common.props reference
            new Replacement($"$(MSBuildProjectDirectory)/../{templateName}.sln", $"$(MSBuildProjectDirectory)/../{newProjectName}.sln"),
            // XML namespace replacements
            new Replacement($"xmlns:{templateName}", $"xmlns:{newProjectName}"),
            new Replacement($"xmlns:ns={templateName}", $"xmlns:ns={newProjectName}"),
            // Class name variations
            new Replacement($"public partial class {templateName}Configuration", $"public partial class {newProjectName}Configuration"),
            // Method names
            new Replacement($"{templateName}Consts", $"{newProjectName}Consts"),
            // Constant replacements
            new Replacement($@"{templateName}Consts""", $@"{newProjectName}Consts"""),
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