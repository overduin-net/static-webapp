public interface ILoggerHelper
{
    void LogDebug(string message);
    void LogInformation(string message);
    void LogTrace(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogError(Exception exception, string message);
    void LogCritical(string message);
}

public class LoggerHelper : ILoggerHelper
{
    private readonly ILogger<LoggerHelper> _logger;

    public LoggerHelper(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<LoggerHelper>();
    }

    public void LogDebug(string message)
    {
        _logger.LogDebug(message);
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogTrace(string message)
    {
        _logger.LogTrace(message);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    public void LogError(string message)
    {
        _logger.LogError(message);
    }

    public void LogError(Exception exception, string message)
    {
        _logger.LogError(exception, message);
    }

    public void LogCritical(string message)
    {
        _logger.LogCritical(message);
    }
}
