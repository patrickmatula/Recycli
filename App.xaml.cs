using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Recycli.Model;
using System.Diagnostics;
using System.Windows;

namespace Recycli
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ViewModel>();
                services.AddSingleton<RecycleManager>();
                services.AddLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Information)
                            .AddTraceSource(new SourceSwitch("RecycliSwitch", "Information"))
                            .AddDebug();
                });
            })
            .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            var logger = _host.Services.GetRequiredService<ILogger<App>>();
            logger.LogInformation("Application started");

            var mainWindow = new MainWindow
            {
                DataContext = _host.Services.GetRequiredService<ViewModel>()
            };
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            var logger = _host.Services.GetRequiredService<ILogger<App>>();
            logger.LogInformation("Application shutting down");

            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }

}
