using SecureMessageManager.Api.Entities;
using SecureMessageManager.Api.Repositories.Interfaces.Communication;
using SecureMessageManager.Api.Repositories.Interfaces.Helpers;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Patch;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Response;

namespace SecureMessageManager.Api.Services.Communication
{
    /// <summary>
    /// Сервис для работы с сообщениями.
    /// </summary>
    /// <param name="checkExistRepository">For DI.</param>
    /// <param name="messageRepository">For DI.</param>
    public class MessageService(ICheckExistRepository checkExistRepository,
                                IMessageRepository messageRepository) : IMessageService
    {
        private readonly ICheckExistRepository _checkExistRepository = checkExistRepository;
        private readonly IMessageRepository _messageRepository = messageRepository;

        /// <summary>
        /// Асинхронно получает коллекцию сообщений чата с пагинацией.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="skip">С какого индекса начать.</param>
        /// <param name="take">Сколько взять.</param>
        /// <returns>Коллекция сообщений чата.</returns>
        public async Task<ICollection<GetMessageResponseDto>> GetChatMessagesAsync(Guid chatId, int skip, int take)
        {
            await _checkExistRepository.IsChatExist(chatId);

            return (await _messageRepository.GetChatMessagesAsync(chatId, skip, take)).Select(m => new GetMessageResponseDto()
            {
                AESKeyEnc = m.AESKeyEnc,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
                ContentEnc = m.ContentEnc,
                Id = m.Id,
                IsRead = m.IsRead,
                IsUpdated = m.IsUpdated,
                SenderId = m.SenderId
            }).ToList();
        }

        /// <summary>
        /// Асинхронно создаёт сообщение.
        /// </summary>
        /// <param name="id">Id чата.</param>
        /// <param name="messageDto">Сообщение для создания.</param>
        /// <returns>MessageCreatedResponseDto.</returns>
        public async Task<MessageCreatedResponseDto> SendMessageAsync(Guid id, SendMessageDto messageDto)
        {
            await _checkExistRepository.IsUserExist(messageDto.SenderId);
            await _checkExistRepository.IsChatExist(id);
            await _checkExistRepository.IsMemberExist(id, messageDto.SenderId);

            var message = new Message
            {
                AESKeyEnc = messageDto.AESKeyEnc,
                ChatId = id,
                ContentEnc = messageDto.ContentEnc,
                SenderId = messageDto.SenderId,
                CreatedAt = messageDto.SentAt
            };

            await _messageRepository.CreateMessageAsync(message);

            return new MessageCreatedResponseDto()
            {
                AESKeyEnc = message.AESKeyEnc,
                CreatedAt = message.CreatedAt,
                UpdatedAt = message.UpdatedAt,
                ChatId = message.ChatId,
                ContentEnc = message.ContentEnc,
                Id = message.Id,
                IsRead = message.IsRead,
                IsUpdated = message.IsUpdated,
                SenderId = message.SenderId
            };
        }

        /// <summary>
        /// Асинхронно изменяет сообщение.
        /// </summary>
        /// <param name="mesId">Id Сообщения.</param>
        /// <param name="patch">Патч для сообщения.</param>
        /// <returns>PatchMessageResponseDto.</returns>
        public async Task<PatchMessageResponseDto> PatchMessageAsync(Guid mesId, PatchMessageDto patch)
        {
            await _checkExistRepository.IsMessageExist(mesId);

            Message message = await _messageRepository.GetMessageByIdAsync(mesId);

            if (message.SenderId != patch.PatcherId)
            {
                throw new InvalidOperationException("Вы не являетесь автором сообщения.");
            }

            message.UpdatedAt = DateTime.UtcNow;
            message.ContentEnc = patch.ContentEnc;

            await _messageRepository.UpdateMessageAsync(message);

            return new PatchMessageResponseDto()
            {
                UpdatedAt = message.UpdatedAt,
                ContentEnc = message.ContentEnc
            };
        }

        /// <summary>
        /// Асинхронно удаляет сообщение.
        /// </summary>
        /// <param name="mesId">Id сообщения.</param>
        public async Task DeleteMessageAsync(Guid mesId)
        {
            await _checkExistRepository.IsMessageExist(mesId);
            await _messageRepository.DeleteMessageByIdAsync(mesId);
        }
    }
}
