using SecureMessageManager.Shared.DTOs.Auth;
using SecureMessageManager.Shared.DTOs.Auxiliary.DeviceInfo;

namespace SecureMessageManager.Api.Services.Interfaces.Auth
{
    /// <summary>
    /// Интерфейс сервиса для управления регистрацией и авторизацией пользователей.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Асинхронно регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="dto"> Данные для регистрации пользователя. </param>
        Task<UserResponseDto> RegisterAsync(RegisterRequestDto dto);

        /// <summary>
        /// Асинхронно авторизует пользователя и выдает JWT токен.
        /// </summary>
        /// <param name="dto">Данные для авторизации пользователя.</param>
        /// <param name="deviceInfo">Информация об устройстве.</param>
        /// <returns>JWT токен при успешной авторизации.</returns>
        Task<AuthResponseDto> AuthorizationAsync(AuthorizationDto dto, DeviceInfoDto deviceInfo);

        /// <summary>
        /// Асинхронное обновление токенов.
        /// </summary>
        /// <param name="incomingRefreshToken">Текущий refresh токен.</param>
        /// <returns>Dto с новыми токенами.</returns>
        Task<RefreshDto> RefreshAsync(string incomingRefreshToken);

        /// <summary>
        /// Асинхронный разлогин конкретной сессии.
        /// </summary>
        /// <param name="sessionId">Id сессии, которую надо прервать.</param>
        Task RevokeSessionAsync(Guid sessionId);

        /// <summary>
        /// Асинхронный разлогин всех сессий пользователя, кроме указанной.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="keepSessionId">Id сессии, которую нужно оставить.</param>
        Task RevokeOtherSessionsAsync(Guid userId, Guid keepSessionId);

        /// <summary>
        /// Асинхронный разлогин всех сессий пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        Task RevokeAllSessionsAsync(Guid userId);
    }
}
