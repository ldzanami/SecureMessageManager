using Microsoft.AspNetCore.SignalR;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming;

namespace SecureMessageManager.Api.Hubs
{
    /// <summary>
    /// Объект хаба.
    /// </summary>
    public class ChatHub(IChatMemberService chatMemberService) : Hub
    {
        private readonly IChatMemberService _chatMemberService = chatMemberService;

        /// <summary>
        /// Вызывается при подключении.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Вызывается при присоединении к чату.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        /// <summary>
        /// Метод отправки сообщения.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <param name="senderId">Id отправителя.</param>
        /// <param name="contentEnc">Зашифрованное сообщение.</param>
        public async Task SendMessage(Guid chatId, Guid senderId, byte[] contentEnc)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", new SendMessageDto
            {
                ChatId = chatId,
                SenderId = senderId,
                ContentEnc = contentEnc,
                SentAt = DateTime.UtcNow
            });
        }
    }
}
