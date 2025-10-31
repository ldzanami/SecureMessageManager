using SecureMessageManager.Api.Entities;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Get;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Get.Response;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Response;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Response;

namespace SecureMessageManager.Api.Services.Interfaces.Communication
{
    /// <summary>
    /// Интерфейс сервиса для работы с чатами.
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// Асинхронно создаёт чат.
        /// </summary>
        /// <param name="dto">Данные чата.</param>
        /// <returns>ChatCreatedResponseDto.</returns>
        Task<ChatCreatedResponseDto> CreateChatAsync(CreateChatDto dto);

        /// <summary>
        /// Асинхронно получает информацию о чате по Id.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <returns>Информация о чате по Id.</returns>
        Task<GetChatResponseDto> GetChatInfoAsync(Guid chatId);

        /// <summary>
        /// Асинхронно получает информацию о чатах пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Коллекция чатов пользователя.</returns>
        Task<ICollection<GetChatResponseDto>> GetUserChatsAsync(Guid userId);
    }
}
