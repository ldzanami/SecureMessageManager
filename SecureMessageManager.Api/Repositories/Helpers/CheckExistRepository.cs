using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.Helpers;

namespace SecureMessageManager.Api.Repositories.Helpers
{
    /// <summary>
    /// Вспомогательный репозиторий, который проверяет наличие объектов в БД.
    /// </summary>
    /// <param name="appDbContext">Контекст БД приложения.</param>
    public class CheckExistRepository(AppDbContext appDbContext) : ICheckExistRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        /// <summary>
        /// Асинхронно проверяет наличие пользователя в БД.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <exception cref="KeyNotFoundException">В случае если пользователь в БД не найден.</exception>
        public async Task IsUserExist(Guid userId)
        {
            if (!await _appDbContext.Users.AnyAsync(u => u.Id == userId))
            {
                throw new KeyNotFoundException($"Пользователя с таким Id: '{userId}' не существует.");
            }
        }


        /// <summary>
        /// Асинхронно проверяет наличие чата в БД.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <exception cref="KeyNotFoundException">В случае если чат в БД не найден.</exception>
        public async Task IsChatExist(Guid chatId)
        {
            if (!await _appDbContext.Chats.AnyAsync(c => c.Id == chatId))
            {
                throw new KeyNotFoundException($"Чата с таким Id: '{chatId}' не существует.");
            }
        }


        /// <summary>
        /// Асинхронно проверяет наличие сессии в БД.
        /// </summary>
        /// <param name="sessionId">Id сессии.</param>
        /// <exception cref="KeyNotFoundException">В случае если сессия в БД не найдена.</exception>
        public async Task IsSessionExist(Guid sessionId)
        {
            if (!await _appDbContext.Sessions.AnyAsync(s => s.Id == sessionId))
            {
                throw new KeyNotFoundException($"Сессии с таким Id: '{sessionId}' не существует.");
            }
        }


        /// <summary>
        /// Асинхронно проверяет наличие сообщения в БД.
        /// </summary>
        /// <param name="messageId">Id сообщения.</param>
        /// <exception cref="KeyNotFoundException">В случае если сообщение в БД не найдено.</exception>
        public async Task IsMessageExist(Guid messageId)
        {
            if (!await _appDbContext.Messages.AnyAsync(m => m.Id == messageId))
            {
                throw new KeyNotFoundException($"Сообщения с таким Id: '{messageId}' не существует.");
            }
        }


        /// <summary>
        /// Асинхронно проверяет наличие файла в БД.
        /// </summary>
        /// <param name="fileId">Id файла.</param>
        /// <exception cref="KeyNotFoundException">В случае если файл в БД не найден.</exception>
        public async Task IsFileExist(Guid fileId)
        {
            if (!await _appDbContext.Files.AnyAsync(f => f.Id == fileId))
            {
                throw new KeyNotFoundException($"Файла с таким Id: '{fileId}' не существует.");
            }
        }


        /// <summary>
        /// Асинхронно проверяет наличие лога в БД.
        /// </summary>
        /// <param name="logId">Id лога.</param>
        /// <exception cref="KeyNotFoundException">В случае если лог в БД не найден.</exception>
        public async Task IsLogExist(Guid logId)
        {
            if (!await _appDbContext.Logs.AnyAsync(l => l.Id == logId))
            {
                throw new KeyNotFoundException($"Лога с таким Id: '{logId}' не существует.");
            }
        }


        /// <summary>
        /// Асинхронно проверяет наличие участника в чате.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="memberId">Id участника.</param>
        /// <exception cref="KeyNotFoundException">В случае если участник в чате не найден.</exception>
        public async Task IsMemberExist(Guid chatId, Guid memberId)
        {
            if (!await _appDbContext.ChatMembers.AnyAsync(cm => cm.ChatId == chatId && cm.UserId == memberId))
            {
                throw new KeyNotFoundException($"В чате с Id: '{chatId}' нет участника с Id: '{memberId}'");
            }
        }
    }
}
