using SecureMessageManager.Api.Entities;

namespace SecureMessageManager.Api.Repositories.Interfaces.Communication
{
    /// <summary>
    /// Интерфейс репозитория для работы с чатами.
    /// </summary>
    public interface IChatRepository
    {
        /// <summary>
        /// Асинхронно создаёт чат.
        /// </summary>
        /// <param name="chat">Чат для создания.</param>
        Task CreateChatAsync(Chat chat);

        /// <summary>
        /// Асинхронно получает информацию о чате.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <returns>GetChatResponseDto.</returns>
        Task<Chat> GetChatInfoAsync(Guid chatId);

        /// <summary>
        /// Асинхронно получает чаты пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Коллекция чатов пользователя.</returns>
        Task<ICollection<Chat>> GetUserChatsAsync(Guid userId);

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
    }
}
