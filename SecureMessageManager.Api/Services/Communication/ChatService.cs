using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.Communication;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Get.Response;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Response;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Response;
using System.Net.Http.Headers;

namespace SecureMessageManager.Api.Services.Communication
{
    /// <summary>
    /// Сервис для работы с чатами.
    /// </summary>
    public class ChatService(IChatRepository chatRepository) : IChatService
    {
        private readonly IChatRepository _chatRepository = chatRepository;

        /// <summary>
        /// Асинхронно создаёт чат.
        /// </summary>
        /// <param name="dto">Данные чата.</param>
        /// <returns>ChatCreatedResponseDto.</returns>
        public async Task<ChatCreatedResponseDto> CreateChatAsync(CreateChatDto dto)
        {
            var chat = new Chat
            {
                Name = dto.Name,
                Description = dto.Description,
                Icon = dto.Icon,
                IsGroup = dto.IsGroup
            };

            await _chatRepository.CreateChatAsync(chat);

            return new ChatCreatedResponseDto
            {
                ChatId = chat.Id,
                CreatedAt = chat.CreatedAt,
                Description = chat.Description,
                Icon = chat.Icon,
                Name = chat.Name
            };
        }

        /// <summary>
        /// Асинхронно получает информацию о чате по Id.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <returns>Информация о чате по Id.</returns>
        public async Task<GetChatResponseDto> GetChatInfoAsync(Guid chatId)
        {
            return await _chatRepository.GetChatInfoAsync(chatId);
        }

        /// <summary>
        /// Асинхронно получает информацию о чатах пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Коллекция чатов пользователя.</returns>
        public async Task<ICollection<GetChatResponseDto>> GetUserChatsAsync(Guid userId)
        {
            return await _chatRepository.GetUserChatsAsync(userId);
        }

        /// <summary>
        /// Асинхронно получает коллекцию сообщений чата с пагинацией.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="skip">С какого индекса начать.</param>
        /// <param name="take">Сколько взять.</param>
        /// <returns>Коллекция сообщений чата.</returns>
        public async Task<ICollection<GetMessageResponseDto>> GetChatMessagesAsync(Guid chatId, int skip, int take)
        {

        }

        /// <summary>
        /// Асинхронно создаёт сообщение.
        /// </summary>
        /// <param name="dto">Данные сообщения.</param>
        /// <returns>MessageCreatedResponseDto.</returns>
        public async Task<MessageCreatedResponseDto> CreateMessageAsync(SendMessageDto dto)
        {

        }
    }
}
