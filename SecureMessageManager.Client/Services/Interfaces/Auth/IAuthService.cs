using SecureMessageManager.Shared.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessageManager.Client.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, string AccessToken, string ErrorMessage)> LoginAsync(AuthorizationDto dto);
    }
}
