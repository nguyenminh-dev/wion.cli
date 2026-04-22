using Wion.Cli.Services;

namespace Wion.Cli;

public class TemplateProcessor
{
    private readonly string _templatePath;
    private readonly string _outputPath;
    private readonly FileReplacer _fileReplacer;
    private readonly DirectoryRenamer _directoryRenamer;
    private readonly ILogger _logger;

    private static readonly string[] IgnoredFolders = { "bin", "obj", ".vs", ".idea", "node_modules", ".git" };

    public TemplateProcessor(string templatePath, string outputPath, bool verbose = false)
    {
        _templatePath = templatePath;
        _outputPath = outputPath;
        _fileReplacer = new FileReplacer();
        _directoryRenamer = new DirectoryRenamer();
        _logger = new Logger();
        _logger.SetVerbose(verbose);
    }

    public async Task ProcessAsync(string newProjectName)
    {
        var templateName = "Wion.Template";
        ProcessedCount = 0;

        _logger.LogInfo($"Starting template processing: {templateName} -> {newProjectName}");
        _logger.LogInfo($"Template: {_templatePath}");
        _logger.LogInfo($"Output: {_outputPath}");

        if (Directory.Exists(_outputPath))
        {
            throw new InvalidOperationException($"Output directory already exists: {_outputPath}");
        }

        _logger.LogInfo("Copying template directory...");
        await CopyDirectoryAsync(_templatePath, _outputPath, templateName, newProjectName);

        _logger.LogInfo("Processing files...");
        await ProcessFilesAsync(_outputPath, templateName, newProjectName);

        _logger.LogInfo("Renaming directories...");
        await RenameDirectoriesAsync(_outputPath, templateName, newProjectName);

        _logger.LogInfo("Renaming files...");
        await RenameFilesAsync(_outputPath, templateName, newProjectName);

        _logger.LogInfo($"Processed {ProcessedCount} files/directories");
    }

    private async Task CopyDirectoryAsync(string sourceDir, string targetDir, string templateName, string newProjectName)
    {
        Directory.CreateDirectory(targetDir);
        ProcessedCount++;

        foreach (var directory in Directory.GetDirectories(sourceDir))
        {
            var dirName = Path.GetFileName(directory);
            if (Array.Exists(IgnoredFolders, f => f.Equals(dirName, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.LogDebug($"Skipped ignored folder: {dirName}");
                continue;
            }

            var newDirName = dirName.Replace(templateName, newProjectName);
            var newTargetDir = Path.Combine(targetDir, newDirName);

            await CopyDirectoryAsync(directory, newTargetDir, templateName, newProjectName);
        }

        foreach (var file in Directory.GetFiles(sourceDir))
        {
            var fileName = Path.GetFileName(file);
            if (Array.Exists(IgnoredFolders, f => fileName.StartsWith(f, StringComparison.OrdinalIgnoreCase)))
                continue;

            var targetFile = Path.Combine(targetDir, fileName);
            await File.WriteAllBytesAsync(targetFile, await File.ReadAllBytesAsync(file));
            ProcessedCount++;
        }
    }

    private async Task ProcessFilesAsync(string directory, string templateName, string newProjectName)
    {
        foreach (var file in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
        {
            var extension = Path.GetExtension(file).ToLower();
            if (extension is ".dll" or ".exe" or ".pdb" or ".png" or ".jpg" or ".jpeg" or ".gif" or ".ico" or ".zip")
            {
                _logger.LogDebug($"Skipped binary file: {file}");
                continue;
            }

            await _fileReplacer.ReplaceContentAsync(file, templateName, newProjectName);
        }
    }

    private async Task RenameDirectoriesAsync(string directory, string templateName, string newProjectName)
    {
        var directories = Directory.GetDirectories(directory, "*", SearchOption.AllDirectories)
            .OrderByDescending(d => d.Length);

        await _directoryRenamer.RenameDirectoriesAsync(directories, templateName, newProjectName);
    }

    private async Task RenameFilesAsync(string directory, string templateName, string newProjectName)
    {
        var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories)
            .Where(f => Path.GetFileName(f).Contains(templateName));

        await _directoryRenamer.RenameFilesAsync(files, templateName, newProjectName);
    }

    public int ProcessedCount { get; private set; }
}