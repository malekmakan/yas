namespace UrlShortener.Utility
{
    using NLog;

    /// <summary>
    /// Log singletone, easily use NLog
    /// </summary>
    public static class Log
    {
        static Log()
        {
            LogManager.ReconfigExistingLoggers();
            Instance = LogManager.GetCurrentClassLogger();
        }

        public static Logger Instance { get; private set; }
    }
}