using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace L2Logger
{
    public class ClassLoggerConfigurator
    {
        public static void ConfigureClassLogger(string logFilePath)
        {
            var configuration = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            configuration.AddTarget("console", consoleTarget);

            var fileTarget = new AsyncTargetWrapper(new FileTarget()
            {
                FileName = logFilePath,
                Layout = @"${date:format=HH\:mm\:ss} ${level:uppercase=true}: ${logger} ${message}",
            });

            configuration.AddTarget("file", fileTarget);

            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${level:uppercase=true}: ${message}";
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule("OK", ConsoleOutputColor.Green, ConsoleOutputColor.NoChange));
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule("ERROR", ConsoleOutputColor.Red, ConsoleOutputColor.NoChange));
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule("WARN", ConsoleOutputColor.Yellow, ConsoleOutputColor.NoChange));
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule("GAME>>AUTH", ConsoleOutputColor.DarkGreen, ConsoleOutputColor.NoChange));
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule("AUTH>>GAME", ConsoleOutputColor.DarkGreen, ConsoleOutputColor.NoChange));
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule("GS>>CLIENT", ConsoleOutputColor.DarkGreen, ConsoleOutputColor.NoChange));
            consoleTarget.WordHighlightingRules.Add(new ConsoleWordHighlightingRule("CLIENT>>GS", ConsoleOutputColor.DarkGreen, ConsoleOutputColor.NoChange));

            configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));
            configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            
            LogManager.Configuration = configuration;
        }
    }
}