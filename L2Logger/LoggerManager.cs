using NLog;
using System.Diagnostics;
using System.Xml.Linq;

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

        public static void Debug(string text)
        {
            LogManager.GetCurrentClassLogger().Debug(text);
        }

        public static void Error(string text)
        {
            LogManager.GetCurrentClassLogger().Error(text);
        }

        public static void ErrorTrace(string text)
        {
            StackTrace stackTrace = new StackTrace();
            LogManager.GetCurrentClassLogger().Error($"{stackTrace.GetFrame(3).GetMethod().ReflectedType.FullName} > {stackTrace.GetFrame(2).GetMethod().ReflectedType.Name} - {text}");
        }

        public static void Warn(string text)
        {
            LogManager.GetCurrentClassLogger().Warn(text);
        }
    }
}