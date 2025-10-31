using Microsoft.EntityFrameworkCore;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.Communication;

namespace SecureMessageManager.Api.Repositories.Communication
{
    /// <summary>
    /// Репозиторий для работы с сообщениями.
    /// </summary>
    /// <param name="appDbContext">For DI.</param>
    public class MessageRepository(AppDbContext appDbContext) : IMessageRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        /// <summary>
        /// Асинхронно получает коллекцию сообщений чата с пагинацией.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="skip">С какого индекса начать.</param>
        /// <param name="take">Сколько взять.</param>
        /// <returns>Коллекция сообщений чата.</returns>
        public async Task<ICollection<Message>> GetChatMessagesAsync(Guid chatId, int skip, int take)
        {
            return await _appDbContext.Messages.Where(m => m.ChatId == chatId)
                                               .Skip(skip)
                                               .Take(take)
                                               .ToListAsync();
        }

        /// <summary>
        /// Асинхронно создаёт сообщение.
        /// </summary>
        /// <param name="message">Сообщение для создания.</param>
        public async Task CreateMessageAsync(Message message)
        {
            await _appDbContext.AddAsync(message);
            await _appDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Асинхронно получает сообщение по Id.
        /// </summary>
        /// <param name="id">Id собщения.</param>
        /// <returns>Сообщение по Id.</returns>
        public async Task<Message> GetMessageByIdAsync(Guid id)
        {
            return await _appDbContext.Messages.FindAsync(id);
        }
        
        /// <summary>
        /// Асинхронно обновляет сообщение.
        /// </summary>
        /// <param name="message">Сообщение для обновления.</param>
        public async Task UpdateMessageAsync(Message message)
        {
            var m = await _appDbContext.Messages.FindAsync(message.Id);
            _appDbContext.Messages.Update(m);
            await _appDbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Асинхронно удаляет сообщение.
        /// </summary>
        /// <param name="id">Id сообщения.</param>
        public async Task DeleteMessageByIdAsync(Guid id)
        {
            Message message = await _appDbContext.Messages.FindAsync(id);
            _appDbContext.Messages.Remove(message);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
