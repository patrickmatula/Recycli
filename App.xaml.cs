using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace Recycli
{
    public partial class App : Application
    {
        public static ILoggerFactory? LoggerFactory { get; private set; }
        public static ILogger? Logger { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder
                    .SetMinimumLevel(LogLevel.Information)
                    .AddTraceSource(new SourceSwitch("RecycliSwitch", "Information"));
            });

            Logger = LoggerFactory.CreateLogger("Recycli");
            Logger.LogInformation("Recycli: Application started");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Guard.IsNotNull(Logger);
            Logger.LogInformation("Recycli: Application shutting down");
            LoggerFactory?.Dispose();
            base.OnExit(e);
        }
    }

}
