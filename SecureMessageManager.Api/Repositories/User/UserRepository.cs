using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Api.Repositories.Interfaces.User;

namespace SecureMessageManager.Api.Repositories.User
{
    /// <summary>
    /// Репозиторий для работы с пользователем
    /// </summary>
    /// <param name="context">Контекст БД приложения.</param>
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        /// <summary>
        /// Асинхронно добавляет нового пользователя в базу данных.
        /// </summary>
        /// <param name="user">Сущность пользователя.</param>
        public async Task AddAsync(Entities.User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Асинхронно получает пользователя по имени.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <returns>Пользователь, если найден; иначе null.</returns>
        public async Task<Entities.User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.UsernameNormalized == username.ToUpper());
        }

        /// <summary>
        /// Проверяет правильный ли пароль.
        /// </summary>
        /// <param name="password">Пароль пользователя.</param>
        /// <param name="hash">Хеш пароля пользователя.</param>
        /// <returns>true если пароль верен; иначе false.</returns>
        public bool VerifyPassword(string password, byte[] hash)
        {
            return Argon2.Verify(System.Text.Encoding.UTF8.GetString(hash), password);
        }
    }
}
