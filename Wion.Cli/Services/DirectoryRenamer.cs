namespace Wion.Cli.Services;

public class DirectoryRenamer
{
    private readonly ILogger _logger;

    public DirectoryRenamer()
    {
        _logger = new Logger();
    }

    public async Task RenameDirectoriesAsync(IEnumerable<string> directories, string templateName, string newProjectName)
    {
        foreach (var dir in directories)
        {
            var dirName = Path.GetFileName(dir);
            if (dirName.Contains(templateName))
            {
                var newDirName = dirName.Replace(templateName, newProjectName);
                var parentDir = Directory.GetParent(dir)?.FullName;
                var newPath = Path.Combine(parentDir, newDirName);

                if (Directory.Exists(newPath))
                {
                    _logger.LogWarning($"Directory already exists, skipping: {newPath}");
                    continue;
                }

                Directory.Move(dir, newPath);
                _logger.LogInfo($"Renamed directory: {dirName} -> {newDirName}");
            }
        }

        await Task.CompletedTask;
    }

    public async Task RenameFilesAsync(IEnumerable<string> files, string templateName, string newProjectName)
    {
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            if (fileName.Contains(templateName))
            {
                var newFileName = fileName.Replace(templateName, newProjectName);
                var directory = Path.GetDirectoryName(file);
                var newPath = Path.Combine(directory, newFileName);

                if (File.Exists(newPath))
                {
                    _logger.LogWarning($"File already exists, skipping: {newPath}");
                    continue;
                }

                File.Move(file, newPath);
                _logger.LogInfo($"Renamed file: {fileName} -> {newFileName}");
            }
        }

        await Task.CompletedTask;
    }
}