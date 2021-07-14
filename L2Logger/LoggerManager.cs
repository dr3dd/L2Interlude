using NLog;

namespace L2Logger
{
    public static class LoggerManager
    {
        public static Logger GetLogManager()
        {
            return LogManager.GetCurrentClassLogger();
        }

        public static void Info(string text)
        {
            LogManager.GetCurrentClassLogger().Info(text);
        }

        public static void Error(string text)
        {
            LogManager.GetCurrentClassLogger().Error(text);
        }

        public static void Warn(string text)
        {
            LogManager.GetCurrentClassLogger().Warn(text);
        }
    }
}