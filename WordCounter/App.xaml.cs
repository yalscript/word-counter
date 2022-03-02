using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WordCounter.Services;
using WordCounter.ViewModels;
using WordCounter.Views;

namespace WordCounter
{
    /// <summary>
    /// Startup class of the WPF application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Default constructor of the class. Configures the IoC container.
        /// </summary>
        public App()
        {
            ServiceProvider = ConfigureServices();
        }

        /// <summary>
        /// Configures the services of the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register the application ViewModels
            services.AddSingleton<MainViewModel>();

            // Register the Views
            services.AddTransient<MainView>();

            // Register the services
            services.AddSingleton<IFileService, FileService>();

            return services.BuildServiceProvider();
        }
    }
}
