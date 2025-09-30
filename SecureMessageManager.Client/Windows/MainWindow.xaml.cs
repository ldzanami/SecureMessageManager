using SecureMessageManager.Client.Services.Helpers;
using SecureMessageManager.Shared.DTOs.Auth;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using SecureMessageManager.Client.Data;

namespace SecureMessageManager.Client.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _http;

        public MainWindow()
        {
            InitializeComponent();
            _http = new HttpClient { BaseAddress = new Uri("https://localhost:7120/") };
            WindowState = WindowState.Maximized;
        }

        public async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var deviceInfoCollector = new DeviceInfoCollector();
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            var dto = new AuthorizationDto
            {
                Username = username,
                Password = password,
                DeviceInfo = await deviceInfoCollector.GetDeviceInfoAsync(),
                RefreshToken = await SecureDataManager.TryGetSecureDataAsync(FileData.StoragePath, FileData.RefreshTokenFileName)
            };

            try
            {
                var response = await _http.PostAsJsonAsync("api/auth/authorization", dto);

                if (response.IsSuccessStatusCode)
                {

                    var result = await response.Content.ReadAsStringAsync();
                    ResultBox.Text = $"Успех: {result}";
                    var responseContent = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                    await SecureDataManager.WriteSecureDataAsync(responseContent!.RefreshToken,
                                                                 FileData.StoragePath,
                                                                 FileData.RefreshTokenFileName);

                    await SecureDataManager.WriteSecureDataAsync(responseContent!.AccessToken,
                                                                 FileData.StoragePath,
                                                                 FileData.AccessTokenFileName);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ResultBox.Text = $"Ошибка: {error}";
                }
            }
            catch (Exception ex)
            {
                ResultBox.Text = $"Ошибка подключения: {ex.Message}";
            }
        }
    }
}