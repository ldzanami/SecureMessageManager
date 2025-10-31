using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.Communication;

namespace SecureMessageManager.Api.Repositories.Communication
{
    /// <summary>
    /// Репозиторий для работы с чатами.
    /// </summary>
    public class ChatRepository(AppDbContext appDbContext) : IChatRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        /// <summary>
        /// Асинхронно создаёт чат.
        /// </summary>
        /// <param name="chat">Чат для создания.</param>
        public async Task CreateChatAsync(Chat chat)
        {
            await _appDbContext.Chats.AddAsync(chat);
            await _appDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Асинхронно получает информацию о чате.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <returns>GetChatResponseDto.</returns>
        public async Task<Chat> GetChatInfoAsync(Guid chatId)
        {
            var chat = await _appDbContext.Chats.FindAsync(chatId);

            return chat;
        }

        /// <summary>
        /// Асинхронно получает чаты пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Коллекция чатов пользователя.</returns>
        public async Task<ICollection<Chat>> GetUserChatsAsync(Guid userId)
        {
            return await _appDbContext.Chats.Where(c => c.Members.Any(m => m.UserId == userId))
                                            .ToListAsync();
        }
    }
}
