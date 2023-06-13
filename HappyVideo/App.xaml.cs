using HappyVideo.Core.Services;
using HappyVideo.Core.Services.Contracts;
using HappyVideo.Models;
using HappyVideo.Services;
using HappyVideo.ViewModels;
using HappyVideo.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace HappyVideo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        public T GetService<T>()
            where T : class
            => _host.Services.GetService(typeof(T)) as T;

        public App()
        {
            
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder(e.Args)
                    .ConfigureAppConfiguration(c =>
                    {
                        c.SetBasePath(appLocation);
                    })
                    .ConfigureServices(ConfigureServices)
                    .Build();

            await _host.StartAsync();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // TODO: Register your services, viewmodels and pages here

            // App Host
            services.AddHostedService<ApplicationHostService>();

            // Activation Handlers

            
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ISpider, Spider>();
            

            


            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();

            services.AddTransient<SearchPage>();
            services.AddTransient<SearchViewModel>();
            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            _host = null;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
        }
    }
}
