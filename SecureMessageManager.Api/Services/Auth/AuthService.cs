using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.User;
using SecureMessageManager.Api.Services.Interfaces.Auth;
using SecureMessageManager.Api.Services.Interfaces.Encription;
using SecureMessageManager.Shared.DTOs.Auth;
using System.Security.Cryptography;

namespace SecureMessageManager.Api.Services.Auth
{
    /// <summary>
    /// Сервис для управления регистрацией и авторизацией пользователей.
    /// </summary>
    public class AuthService(IUserRepository userRepository,
                             IGeneratorService generatorService,
                             IPasswordHashService passwordHashService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IGeneratorService _generatorService = generatorService;
        private readonly IPasswordHashService _passwordHashService = passwordHashService;

        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="dto">Данные для регистрации пользователя.</param>
        public async Task<UserResponseDto> Register(RegisterRequestDto dto)
        {
            if (_userRepository.GetByUsernameAsync(dto.Username.ToUpper()) != null)
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
        /// Авторизует пользователя и выдает JWT токен.
        /// </summary>
        /// <param name="dto">Данные для авторизации пользователя.</param>
        /// <returns>JWT токен при успешной авторизации.</returns>
        public async Task<AuthResponseDto> Authorization(AuthorizationDto dto)
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

            var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Username);
            var refreshToken = _jwtService.GenerateRefreshToken();
            
            // Новую сессию

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Username = user.Username
            };
        }
    }
}
