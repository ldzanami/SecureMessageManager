using SecureMessageManager.Client.Services.Commands;
using SecureMessageManager.Client.Services.Helpers;
using SecureMessageManager.Client.Services.Interfaces.Auth;
using SecureMessageManager.Shared.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecureMessageManager.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;

        public string Username { get; set; }
        public string Password { get; set; }

        private string _resultMessage;
        public string ResultMessage
        {
            get => _resultMessage;
            set => SetProperty(ref _resultMessage, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(async _ => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            var dto = new AuthorizationDto
            {
                Username = Username,
                Password = Password,
                DeviceInfo = await DeviceInfoService.GetDeviceInfoAsync(),
                RefreshToken = await SecureDataService.TryGetSecureDataAsync(
                    Data.FileData.StoragePath, Data.FileData.RefreshTokenFileName)
            };

            var result = await _authService.LoginAsync(dto);

            if (result.IsSuccess)
                ResultMessage = "Успех: " + result.AccessToken[..20] + "...";
            else
                ResultMessage = "Ошибка: " + result.ErrorMessage;
        }
    }
}
