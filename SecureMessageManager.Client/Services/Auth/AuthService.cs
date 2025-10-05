using SecureMessageManager.Client.Services.Helpers;
using SecureMessageManager.Client.Services.Interfaces.Auth;
using SecureMessageManager.Shared.DTOs.Auth.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Auth.Post.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Client.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<(bool IsSuccess, string AccessToken, string ErrorMessage)> LoginAsync(AuthorizationDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/authorization", dto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                await SecureDataService.WriteSecureDataAsync(result!.RefreshToken, Data.FileData.StoragePath, Data.FileData.RefreshTokenFileName);
                await SecureDataService.WriteSecureDataAsync(result!.AccessToken, Data.FileData.StoragePath, Data.FileData.AccessTokenFileName);

                return (true, result.AccessToken, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
        }
    }
}
