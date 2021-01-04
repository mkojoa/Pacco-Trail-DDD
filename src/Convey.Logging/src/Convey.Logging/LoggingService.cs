namespace Convey.Logging
{
    public interface ILoggingService
    {
         void SetLoggingLevel(string logEventLevel);
    }

    public class LoggingService : ILoggingService
    {
         public void SetLoggingLevel(string logEventLevel)
         {
             Extensions.LoggingLevelSwitch.MinimumLevel = Extensions.GetLogEventLevel(logEventLevel);
         }
    }
}
