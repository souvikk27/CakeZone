namespace Cakezone.Common.Logging;

public interface ILoggerManager
{
    void LogInfo(string message);
    void LogDebug(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogException(Exception ex);
    void LogExcepting(Exception ex, string message);
}