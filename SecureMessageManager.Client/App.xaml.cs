using Microsoft.Extensions.DependencyInjection;
using SecureMessageManager.Client.Services.Auth;
using SecureMessageManager.Client.Services.Interfaces.Auth;
using SecureMessageManager.Client.ViewModels;
using SecureMessageManager.Client.Views;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace SecureMessageManager.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddSingleton(new HttpClient { BaseAddress = new Uri("https://localhost:7120/") });
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainWindow>();

            Services = services.BuildServiceProvider();

            var window = Services.GetRequiredService<MainWindow>();
            window.Show();
        }
    }

}