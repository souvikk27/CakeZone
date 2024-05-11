namespace CakeZone.Services.Product.Services.Logging;

public class LoggerManager : ILoggerManager
{
    private readonly ILogger<LoggerManager> logger;

    public LoggerManager(ILogger<LoggerManager> logger)
    {
        this.logger = logger;
    }


    public void LogInfo(string message)
    {
        throw new NotImplementedException();
    }

    public void LogDebug(string message)
    {
        logger.LogDebug(message);
    }

    public void LogError(string message)
    {
        logger.LogError(message);
    }

    public void LogException(Exception ex)
    {
        logger.LogError(ex, ex.Message);
    }

    public void LogExcepting(Exception ex, string message)
    {
        logger.LogError(ex, message);
    }

    public void LogWarning(string message)
    {
        logger.LogWarning(message);
    }
}