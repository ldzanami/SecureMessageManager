
using Isopoh.Cryptography.Argon2;

namespace SecureMessageManager.Api.Repositories.Interfaces.User
{
    /// <summary>
    /// Интерфейс репозитория для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Асинхронно получает пользователя по имени.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <returns>Пользователь, если найден; иначе null.</returns>
        Task<Entities.User?> GetByUsernameAsync(string username);

        /// <summary>
        /// Асинхронно добавляет нового пользователя в базу данных.
        /// </summary>
        /// <param name="user">Сущность пользователя.</param>
        Task AddAsync(Entities.User user);

        /// <summary>
        /// Проверяет правильный ли пароль.
        /// </summary>
        /// <param name="password">Пароль пользователя.</param>
        /// <param name="hash">Хеш пароля пользователя.</param>
        /// <returns>true если пароль верен; иначе false.</returns>
        public bool VerifyPassword(string password, byte[] hash);
    }
}
