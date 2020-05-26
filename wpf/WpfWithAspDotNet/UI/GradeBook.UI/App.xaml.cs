using System;
using System.Runtime.Remoting.Services;
using System.Windows;
using GradeBook.Core.Migrations;
using GradeBook.UI.Services.Implementations;
using GradeBook.UI.Services.Interfaces;
using GradeBook.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GradeBook.UI
{

    public static class ServiceProviderHelper
    {
        private static ServiceCollection Services;

        private static IServiceProvider ServiceProvider;

        public static void Init()
        {
            Services = new ServiceCollection();
        }

        public static void AddServices()
        {
            Services.AddScoped<IStudentService, StudentService>();

            Services.AddSingleton<MainWindow>();
            Services.AddSingleton<StudentViewModelCollection>();
        }

        public static void Build()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }

        public static TService GetService<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider _serviceProvider { get; private set; }

        public App()
        {
            ServiceProviderHelper.Init();
            ServiceProviderHelper.AddServices();
            ServiceProviderHelper.Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<StudentViewModelCollection>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ServiceProviderHelper.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
