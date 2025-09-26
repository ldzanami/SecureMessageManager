using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.User;
using SecureMessageManager.Api.Services.Interfaces.Auth;
using SecureMessageManager.Api.Services.Interfaces.Encription;
using SecureMessageManager.Shared.DTOs.Auth;
using SecureMessageManager.Shared.DTOs.Auxiliary;

namespace SecureMessageManager.Api.Services.Auth
{
    // TODO: при авторизации с устройства, сессия которого уже есть, просто обновить сессию.

    /// <summary>
    /// Сервис для управления регистрацией и авторизацией пользователей.
    /// </summary>
    public class AuthService(IUserRepository userRepository,
                             IGeneratorService generatorService,
                             IPasswordHashService passwordHashService,
                             ISessionService sessionService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IGeneratorService _generatorService = generatorService;
        private readonly IPasswordHashService _passwordHashService = passwordHashService;
        private readonly ISessionService _sessionService = sessionService;

        /// <summary>
        /// Асинхронно регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="dto">Данные для регистрации пользователя.</param>
        public async Task<UserResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (await _userRepository.GetByUsernameAsync(dto.Username.ToUpper()) != null)
            {
                throw new InvalidOperationException("Пользователь с таким именем уже существует.");
            }

            (var privateKey, var publicKey) = _generatorService.GenerateRSAKeyPair(2048);
            var salt = _generatorService.GenerateSalt(32);

            User user = new()
            {
                Username = dto.Username,
                UsernameNormalized = dto.Username.ToUpper(),
                Salt = salt,
                PublicKey = publicKey,
                PrivateKeyEnc = privateKey,
                PasswordHash = _passwordHashService.HashPassword(dto.Password, salt),
            };

            await _userRepository.AddAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                PublicKey = user.PublicKey
            };
        }

        /// <summary>
        /// Асинхронно авторизует пользователя и выдает JWT токен.
        /// </summary>
        /// <param name="dto">Данные для авторизации пользователя.</param>
        /// <param name="deviceInfo">Информация об устройстве.</param>
        /// <returns>JWT токен при успешной авторизации.</returns>
        public async Task<AuthResponseDto> AuthorizationAsync(AuthorizationDto dto, DeviceInfoDto deviceInfo)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username.ToUpper());

            if (user == null)
            {
                throw new UnauthorizedAccessException("Неверный логин или пароль.");
            }

            bool isPasswordValid = _userRepository.VerifyPassword(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Неверный логин или пароль.");
            }

            (string accessToken, string refreshToken, Guid sesssionId) = await _sessionService.CreateSessionAsync(user, deviceInfo);

            return new AuthResponseDto
            {
                SessionId = sesssionId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Username = user.Username
            };
        }

        /// <summary>
        /// Асинхронное обновление токенов.
        /// </summary>
        /// <param name="incomingRefreshToken">Текущий refresh токен.</param>
        /// <returns>Dto с новыми токенами.</returns>
        public async Task<RefreshDto> RefreshAsync(string incomingRefreshToken)
        {
            (string accessToken, string refreshToken, Guid sessionId) = await _sessionService.RefreshAsync(incomingRefreshToken);

            return new RefreshDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                SessionId = sessionId
            };
        }

        /// <summary>
        /// Асинхронный разлогин конкретной сессии.
        /// </summary>
        /// <param name="sessionId">Id сессии, которую надо прервать.</param>
        public async Task RevokeSessionAsync(Guid sessionId)
        {
            await _sessionService.RevokeSessionAsync(sessionId);
        }

        /// <summary>
        /// Асинхронный разлогин всех сессий пользователя, кроме указанной.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="keepSessionId">Id сессии, которую нужно оставить.</param>
        public async Task RevokeOtherSessionsAsync(Guid userId, Guid keepSessionId)
        {
            await _sessionService.RevokeOtherSessionsAsync(userId, keepSessionId);
        }

        /// <summary>
        /// Асинхронный разлогин всех сессий пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        public async Task RevokeAllSessionsAsync(Guid userId)
        {
            await _sessionService.RevokeAllSessionsAsync(userId);
        }
    }
}
