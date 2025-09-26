using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.User;
using System.Linq;

namespace SecureMessageManager.Api.Repositories.User
{
    /// <summary>
    /// Репозиторий для работы с сессиями.
    /// </summary>
    public class SessionRepository(AppDbContext appDbContext) : ISessionRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        /// <summary>
        /// Асинхронно получает коллекцию сессий пользователя, кроме завершённых.
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Коллекция сессий пользователя, кроме завершённых.</returns>
        public async Task<ICollection<Session>> GetUserSessionsExcludingRevokedAsync(Guid userId)
        {
            return await _appDbContext.Sessions.Where(s => s.UserId == userId && !s.IsRevoked)
                                               .ToListAsync();
        }

        /// <summary>
        /// Асинхронно добавляет сессию.
        /// </summary>
        /// <param name="session">Сессия для добавления.</param>
        public async Task AddSessionAsync(Session session)
        {
            await _appDbContext.Sessions.AddAsync(session);
            await _appDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Асинхронно получает сессию по хешу refresh токена.
        /// </summary>
        /// <param name="refreshHash">Хеш refresh токена</param>
        /// <returns>Сессия с указанным refresh токеном.</returns>
        public async Task<Session> GetSessionByRefreshHashAsync(string refreshHash)
        {
            return await _appDbContext.Sessions.Include(s => s.User)
                                               .FirstOrDefaultAsync(s => s.RefreshToken == refreshHash);
        }

        /// <summary>
        /// Асинхронно обновляет сессию.
        /// </summary>
        /// <param name="session">Сессия для обновления.</param>
        public async Task UpdateSessionAsync(Session session)
        {
            _appDbContext.Sessions.Update(session);
            await _appDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Асинхронно получает сессию по Id.
        /// </summary>
        /// <param name="sessionId">Id сессии.</param>
        /// <returns>Сессия с указанным Id.</returns>
        public async Task<Session> GetSessionByIdAsync(Guid sessionId)
        {
            return await _appDbContext.Sessions.FindAsync(sessionId);
        }

        /// <summary>
        /// Асинхронно получает коллекцию сессий пользвателя, кроме указанной и завершённых.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="keepSessionId">Id игнорируемой сессии.</param>
        /// <returns>Коллекция сессий пользвателя, кроме указанной и завершённых.</returns>
        public async Task<ICollection<Session>> GetAllUserSessionsExcludingSpecifiedAndRevokedAsync(Guid userId, Guid keepSessionId)
        {
            return await _appDbContext.Sessions.Where(s => s.UserId == userId && s.Id != keepSessionId && !s.IsRevoked)
                                               .ToListAsync();
        }

        /// <summary>
        /// Асинхронно обновляет несколько сессий.
        /// </summary>
        /// <param name="sessions">Коллекция сессий для обновления.</param>
        public async Task UpdateSessionsRangeAsync(ICollection<Session> sessions)
        {
            _appDbContext.UpdateRange(sessions);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
