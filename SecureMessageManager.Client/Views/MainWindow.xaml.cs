using SecureMessageManager.Client.Services.Helpers;
using SecureMessageManager.Shared.DTOs.Auth;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using SecureMessageManager.Client.Data;
using SecureMessageManager.Client.ViewModels;

namespace SecureMessageManager.Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(LoginViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
                vm.Password = PasswordBox.Password;
        }
    }
}