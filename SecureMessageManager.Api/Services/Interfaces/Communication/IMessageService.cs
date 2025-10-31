using SecureMessageManager.Shared.DTOs.Communication.Messages.Patch;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Response;

namespace SecureMessageManager.Api.Services.Interfaces.Communication
{
    /// <summary>
    /// Интерфейс сервиса для работы с сообщениями.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Асинхронно создаёт сообщение.
        /// </summary>
        /// <param name="id">Id чата.</param>
        /// <param name="message">Сообщение для создания.</param>
        /// <returns>MessageCreatedResponseDto.</returns>
        Task<MessageCreatedResponseDto> SendMessageAsync(Guid id, SendMessageDto message);

        /// <summary>
        /// Асинхронно изменяет сообщение.
        /// </summary>
        /// <param name="mesId">Id Сообщения.</param>
        /// <param name="patch">Патч для сообщения.</param>
        /// <returns>PatchMessageResponseDto.</returns>
        Task<PatchMessageResponseDto> PatchMessageAsync(Guid mesId, PatchMessageDto patch);

        /// <summary>
        /// Асинхронно удаляет сообщение.
        /// </summary>
        /// <param name="mesId">Id сообщения.</param>
        Task DeleteMessageAsync(Guid mesId);

        /// <summary>
        /// Асинхронно получает коллекцию сообщений чата с пагинацией.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="skip">С какого индекса начать.</param>
        /// <param name="take">Сколько взять.</param>
        /// <returns>Коллекция сообщений чата.</returns>
        Task<ICollection<GetMessageResponseDto>> GetChatMessagesAsync(Guid chatId, int skip, int take);
    }
}
