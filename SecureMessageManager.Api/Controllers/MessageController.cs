using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureMessageManager.Api.Services.Interfaces.Communication;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Patch;
using SecureMessageManager.Shared.DTOs.Communication.Messages.Post.Incoming;

namespace SecureMessageManager.Api.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за работу с сообщениями.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/{chatId}/[controller]")]
    public class MessageController(IMessageService messageService) : ControllerBase
    {
        private readonly IMessageService _messageService = messageService;

        /// <summary>
        /// Get запрос на получение сообщений из чата с пагинацией.
        /// </summary>
        /// <param name="id">Id чата.</param>
        /// <param name="take">Количество получаемых сообщений.</param>
        /// <param name="skip">Сколько сообщений пропустить чтобы взять следующие take.</param>
        /// <returns>Коллекция из take сообщений - ICollection(GetMessageResponseDto)</returns>
        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetChatMessages([FromRoute] Guid id, [FromQuery] int take, [FromQuery] int skip)
        {
            var response = await _messageService.GetChatMessagesAsync(id, take, skip);
            return Ok(response);
        }

        /// <summary>
        /// Post запрос на отправку сообщения.
        /// </summary>
        /// <param name="id">Id чата.</param>
        /// <param name="message">Сообщение для отправки.</param>
        /// <returns>Созданное сообщение.</returns>
        [HttpPost("{id}/messages")]
        public async Task<IActionResult> SendMessage([FromRoute] Guid id, [FromBody] SendMessageDto message)
        {
            var response = await _messageService.SendMessageAsync(id, message);
            return CreatedAtAction(nameof(SendMessage), response);
        }

        /// <summary>
        /// Patch запрос на изменение сообщения.
        /// </summary>
        /// <param name="mesId">Id сообщения.</param>
        /// <param name="patch">Изменённое сообщение.</param>
        [HttpPatch("{mesId}")]
        public async Task<IActionResult> PatchMessage([FromRoute] Guid mesId, [FromBody] PatchMessageDto patch)
        {
            await _messageService.PatchMessageAsync(mesId, patch);
            return NoContent();
        }

        /// <summary>
        /// Delete запрос на удаление сообщения.
        /// </summary>
        /// <param name="mesId">Id сообщения.</param>
        [HttpDelete("{mesId}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid mesId)
        {
            await _messageService.DeleteMessageAsync(mesId);
            return NoContent();
        }
    }
}

//Получение сообщений +
//Отправление сообщений +
//Изменение сообщений +
//Удаление сообщений +
//Пересылка сообщений ?
//Ответ на сообщение ?