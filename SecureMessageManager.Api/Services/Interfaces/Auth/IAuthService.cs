using SecureMessageManager.Shared.DTOs.Auth;

namespace SecureMessageManager.Api.Services.Interfaces.Auth
{
    /// <summary>
    /// Интерфейс сервиса для управления регистрацией и авторизацией пользователей.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="dto"> Данные для регистрации пользователя. </param>
        public Task<UserResponseDto> Register(RegisterRequestDto dto);

        /// <summary>
        /// Авторизует пользователя и выдает JWT токен.
        /// </summary>
        /// <param name="dto">Данные для авторизации пользователя.</param>
        /// <returns>JWT токен при успешной авторизации.</returns>
        Task<AuthResponseDto> Authorization(AuthorizationDto dto);
    }
}
