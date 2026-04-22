namespace Wion.Cli.Services;

public class Logger : ILogger
{
    private bool _verbose;

    public void SetVerbose(bool verbose)
    {
        _verbose = verbose;
    }

    public void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }

    public void LogDebug(string message)
    {
        if (_verbose)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"[DEBUG] {message}");
            Console.ResetColor();
        }
    }

    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARN] {message}");
        Console.ResetColor();
    }

    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {message}");
        Console.ResetColor();
    }
}

public interface ILogger
{
    void LogInfo(string message);
    void LogDebug(string message);
    void LogWarning(string message);
    void LogError(string message);
}