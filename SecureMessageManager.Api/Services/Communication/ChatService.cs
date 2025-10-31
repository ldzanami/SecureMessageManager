using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.Communication;
using SecureMessageManager.Api.Repositories.Interfaces.Helpers;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Get.Response;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Response;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Response;
using System;

namespace SecureMessageManager.Api.Services.Communication
{
    /// <summary>
    /// Сервис для работы с чатами.
    /// </summary>
    public class ChatService(IChatRepository chatRepository, ICheckExistRepository checkExistRepository) : IChatService
    {
        private readonly IChatRepository _chatRepository = chatRepository;
        private readonly ICheckExistRepository _checkExistRepository = checkExistRepository;

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
                IsGroup = dto.IsGroup,
                CreatorId = dto.CreatorId
            };

            await _chatRepository.CreateChatAsync(chat);

            return new ChatCreatedResponseDto
            {
                ChatId = chat.Id,
                CreatedAt = chat.CreatedAt,
                Description = chat.Description,
                Icon = chat.Icon,
                Name = chat.Name,
                CreatorId = chat.CreatorId
            };
        }

        /// <summary>
        /// Асинхронно получает информацию о чате по Id.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <returns>Информация о чате по Id.</returns>
        public async Task<GetChatResponseDto> GetChatInfoAsync(Guid chatId)
        {
            await _checkExistRepository.IsChatExist(chatId);

            var chat = await _chatRepository.GetChatInfoAsync(chatId);

            return new GetChatResponseDto
            {
                ChatId = chat.Id,
                CreatedAt = chat.CreatedAt,
                UpdatedAt = chat.UpdatedAt,
                Description = chat.Description,
                Icon= chat.Icon,
                IsGroup = chat.IsGroup,
                Name = chat.Name
            };
        }

        /// <summary>
        /// Асинхронно получает информацию о чатах пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Коллекция чатов пользователя.</returns>
        public async Task<ICollection<GetChatResponseDto>> GetUserChatsAsync(Guid userId)
        {
            await _checkExistRepository.IsUserExist(userId);

            return (await _chatRepository.GetUserChatsAsync(userId)).Select(c => new GetChatResponseDto()
            {
                ChatId = c.Id,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Description = c.Description,
                Icon = c.Icon,
                IsGroup = c.IsGroup,
                Name = c.Name
            }).ToList();
        }
    }
}
