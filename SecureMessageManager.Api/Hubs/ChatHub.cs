using Microsoft.AspNetCore.SignalR;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication;

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
        /// <param name="userId">Id пользователя.</param>
        /// <param name="contentEnc">Зашифрованное сообщение.</param>
        /// <returns></returns>
        public async Task SendMessage(string chatId, string userId, string contentEnc)
        {
            await Clients.Group(chatId).SendAsync("ReceiveMessage", new SendMessageDto
            {
                ChatId = chatId,
                UserId = userId,
                ContentEnc = contentEnc,
                SentAt = DateTime.UtcNow
            });
        }
    }
}
