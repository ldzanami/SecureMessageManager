using SecureMessageManager.Api.Entities;

namespace SecureMessageManager.Api.Repositories.Interfaces.Communication
{
    /// <summary>
    /// Интерфейс репозитория для работы с сообщениями.
    /// </summary>
    public interface IMessageRepository
    {


        /// <summary>
        /// Асинхронно получает коллекцию сообщений чата с пагинацией.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="skip">С какого индекса начать.</param>
        /// <param name="take">Сколько взять.</param>
        /// <returns>Коллекция сообщений чата.</returns>
        Task<ICollection<Message>> GetChatMessagesAsync(Guid chatId, int skip, int take);

        /// <summary>
        /// Асинхронно создаёт сообщение.
        /// </summary>
        /// <param name="message">Сообщение для создания.</param>
        Task CreateMessageAsync(Message message);

        /// <summary>
        /// Асинхронно получает сообщение по Id.
        /// </summary>
        /// <param name="id">Id собщения.</param>
        /// <returns>Сообщение по Id.</returns>
        Task<Message> GetMessageByIdAsync(Guid id);

        /// <summary>
        /// Асинхронно обновляет сообщение.
        /// </summary>
        /// <param name="message">Сообщение для обновления.</param>
        Task UpdateMessageAsync(Message message);

        /// <summary>
        /// Асинхронно удаляет сообщение.
        /// </summary>
        /// <param name="id">Id сообщения.</param>
        Task DeleteMessageByIdAsync(Guid id);
    }
}
