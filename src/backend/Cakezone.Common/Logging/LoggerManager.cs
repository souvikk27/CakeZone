using Serilog;
namespace Cakezone.Common.Logging;

public class LoggerManager : ILoggerManager
{
    private readonly ILogger logger;

    public LoggerManager(ILogger logger)
    {
        this.logger = logger;
    }

    public void LogInfo(string message)
    {
        throw new NotImplementedException();
    }

    public void LogDebug(string message)
    {
        logger.Debug(message);
    }

    public void LogError(string message)
    {
        logger.Error(message);
    }

    public void LogException(Exception ex)
    {
        logger.Error(ex, ex.Message);
    }

    public void LogExcepting(Exception ex, string message)
    {
        logger.Error(ex, message);
    }

    public void LogWarning(string message)
    {
        logger.Warning(message);
    }
}