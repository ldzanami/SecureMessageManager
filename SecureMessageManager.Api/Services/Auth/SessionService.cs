using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.User;
using SecureMessageManager.Api.Services.Interfaces.Auth;

namespace SecureMessageManager.Api.Services.Auth
{
    /// <summary>
    /// Сервис управления сессиями.
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly IJWTGeneratorService _jwtGeneratorService;
        private readonly TimeSpan _refreshLifetime;
        private readonly ISessionRepository _sessionRepository;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="sessionRepository">For DI.</param>
        /// <param name="jwtGeneratorService">For DI.</param>
        /// <param name="configuration">For DI.</param>
        /// <exception cref="ArgumentNullException">DI не передал компоненты.</exception>
        public SessionService(ISessionRepository sessionRepository, IJWTGeneratorService jwtGeneratorService, IConfiguration configuration)
        {
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _jwtGeneratorService = jwtGeneratorService ?? throw new ArgumentNullException(nameof(jwtGeneratorService));
            var refreshTokenLifetimeDays = int.Parse(configuration["Jwt:RefreshTokenLifetimeDays"] ?? "7");
            _refreshLifetime = TimeSpan.FromDays(refreshTokenLifetimeDays);
        }

        /// <summary>
        /// Асинхронно создаёт сессию.
        /// </summary>
        /// <param name="user">Пользователь сессии.</param>
        /// <param name="deviceInfo">Информация об устройстве.</param>
        /// <returns>access + refresh токены и id сессии.</returns>
        public async Task<(string AccessToken, string RefreshToken, Guid SessionId)> CreateSessionAsync(User user, string deviceInfo)
        {
            var access = _jwtGeneratorService.GenerateAccessToken(user, out _);
            var refreshPlain = _jwtGeneratorService.GenerateRefreshToken();
            var refreshHash = _jwtGeneratorService.HashToken(refreshPlain);

            var session = new Session
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RefreshToken = refreshHash,
                ExpiresAt = DateTime.UtcNow + _refreshLifetime,
                IsRevoked = false,
                DeviceInfo = deviceInfo,
                CreatedAt = DateTime.UtcNow
            };

            await _sessionRepository.AddSessionAsync(session);

            return (access, refreshPlain, session.Id);
        }

        /// <summary>
        /// Асинхронная ротация refresh: валидирует входной refresh, если ок — меняет на новый и отдаёт новую пару.
        /// </summary>
        /// <param name="incomingRefreshToken">Текущий refresh токен.</param>
        /// <returns>новые access + refresh токены и id сессии.</returns>
        /// <exception cref="UnauthorizedAccessException">Некорректный токен.</exception>
        public async Task<(string AccessToken, string RefreshToken, Guid SessionId)> RefreshAsync(string incomingRefreshToken)
        {
            var incomingHash = _jwtGeneratorService.HashToken(incomingRefreshToken);

            var session = await _sessionRepository.GetSessionByRefreshHashAsync(incomingHash);

            if (session == null) throw new UnauthorizedAccessException("Refresh токен не найден");
            if (session.IsRevoked) throw new UnauthorizedAccessException("Refresh токен отозван");
            if (session.ExpiresAt <= DateTime.UtcNow) throw new UnauthorizedAccessException("Refresh токен просрочен");

            var newRefreshPlain = _jwtGeneratorService.GenerateRefreshToken();
            session.RefreshToken = _jwtGeneratorService.HashToken(newRefreshPlain);
            session.ExpiresAt = DateTime.UtcNow + _refreshLifetime;

            await _sessionRepository.UpdateSessionAsync(session);

            var access = _jwtGeneratorService.GenerateAccessToken(session.User, out _);
            return (access, newRefreshPlain, session.Id);
        }

        /// <summary>
        /// Асинхронный разлогин конкретной сессии.
        /// </summary>
        /// <param name="sessionId">Id сессии, которую надо прервать.</param>
        public async Task RevokeSessionAsync(Guid sessionId)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(sessionId);
            if (session == null) return;
            session.IsRevoked = true;
            await _sessionRepository.UpdateSessionAsync(session);
        }

        /// <summary>
        /// Асинхронный разлогин всех сессий пользователя, кроме указанной.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="keepSessionId">Id сессии, которую нужно оставить.</param>
        public async Task RevokeOtherSessionsAsync(Guid userId, Guid keepSessionId)
        {
            var sessions = await _sessionRepository.GetAllUserSessionsExcludingSpecifiedAndRevokedAsync(userId, keepSessionId);

            foreach (var s in sessions) s.IsRevoked = true;
            await _sessionRepository.UpdateSessionsRangeAsync(sessions);
        }

        /// <summary>
        /// Асинхронный разлогин всех сессий пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        public async Task RevokeAllSessionsAsync(Guid userId)
        {
            var sessions = await _sessionRepository.GetUserSessionsExcludingRevokedAsync(userId);

            foreach (var s in sessions) s.IsRevoked = true;
            await _sessionRepository.UpdateSessionsRangeAsync(sessions);
        }
    }
}
