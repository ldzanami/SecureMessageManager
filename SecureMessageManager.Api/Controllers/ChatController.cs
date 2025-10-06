using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Incoming;

namespace SecureMessageManager.Api.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за работу с чатами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController(IChatService chatService) : ControllerBase
    {
        private readonly IChatService _chatService = chatService;

        /// <summary>
        /// Post запрос на создание чата.
        /// </summary>
        /// <param name="dto">Данные для создания чата.</param>
        /// <returns>201 Созданное сообщение - ChatCreatedResponseDto.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat(CreateChatDto dto)
        {
            var response = await _chatService.CreateChatAsync(dto);
            return CreatedAtAction(nameof(CreateChat), response);
        }

        /// <summary>
        /// Get запрос на получение информации о чате.
        /// </summary>
        /// <param name="chatId">Id чата.</param>
        /// <returns>200 Информацию о чате - GetChatResponseDto.</returns>
        [HttpGet("info")]
        public async Task<IActionResult> GetChatInfo(Guid chatId)
        {
            var response = await _chatService.GetChatInfoAsync(chatId);
            return Ok(response);
        }
    }
}
