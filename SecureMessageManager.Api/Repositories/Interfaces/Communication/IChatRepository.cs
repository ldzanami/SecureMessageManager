using SecureMessageManager.Api.Entities;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Get.Response;

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
        Task<GetChatResponseDto> GetChatInfoAsync(Guid chatId);

        /// <summary>
        /// Асинхронно получает чаты пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Коллекция чатов пользователя.</returns>
        Task<ICollection<GetChatResponseDto>> GetUserChatsAsync(Guid userId);
    }
}
