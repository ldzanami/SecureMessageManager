using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication.Chats.Post.Incoming;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SecureMessageManager.Api.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за работу с чатами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController(IChatService chatService) : ControllerBase
    {
        private readonly IChatService _chatService = chatService;

        /// <summary>
        /// Post запрос на создание чата.
        /// </summary>
        /// <param name="dto">Данные для создания чата.</param>
        /// <returns>201 Созданное сообщение - ChatCreatedResponseDto.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto dto)
        {
            var response = await _chatService.CreateChatAsync(dto);
            return CreatedAtAction(nameof(CreateChat), response);
        }

        /// <summary>
        /// Get запрос на получение информации о чате.
        /// </summary>
        /// <param name="id">Id чата.</param>
        /// <returns>200 Информацию о чате - GetChatResponseDto.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChatInfo([FromRoute] Guid id)
        {
            var response = await _chatService.GetChatInfoAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Get запрос на получение своих чатов.
        /// </summary>
        /// <returns>200 Коллекция своих чатов - ICollection(GetChatResponseDto).</returns>
        [HttpGet]
        public async Task<IActionResult> GetMyChats()
        {
            var id = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _chatService.GetUserChatsAsync(id);
            return Ok(response);
        }
    }
}
