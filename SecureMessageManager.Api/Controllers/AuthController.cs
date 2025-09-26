using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureMessageManager.Api.Services.Interfaces.Auth;
using SecureMessageManager.Shared.DTOs.Auth;
using SecureMessageManager.Shared.DTOs.Auxiliary;

namespace SecureMessageManager.Api.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за регистрацию и авторизацию пользователя в системе.
    /// </summary>
    /// <param name="authService">Сервис авторизации.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Post запрос для регистрации пользователя.
        /// </summary>
        /// <param name="dto">Dto с данными для регистрации.</param>
        /// <returns>200 если удачно;<br>400 если ошибка;</br></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                var response = await _authService.RegisterAsync(dto);
                return Ok(response);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Post запрос для авторизации пользователя.
        /// </summary>
        /// <param name="dto">Dto с данными для авторизации.</param>
        /// <param name="deviceInfo">Dto с данными об устройстве.</param>
        /// <returns>200 если удачно;<br>400 если ошибка;</br></returns>
        [HttpPost("authorization")]
        public async Task<IActionResult> Authorization([FromBody] AuthorizationDto dto)
        {
            try
            {
                var response = await _authService.AuthorizationAsync(dto, dto.DeviceInfo);
                return Ok(response);
            }
            catch(UnauthorizedAccessException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Post запрос на обновление токенов.
        /// </summary>
        /// <param name="incomingRefreshToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string incomingRefreshToken)
        {
            try
            {
                var response = await _authService.RefreshAsync(incomingRefreshToken);
                return Ok(response);
            }
            catch(UnauthorizedAccessException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Post запрос на разлогин конкретной сессии.
        /// </summary>
        /// <param name="sessionId">Id сессии, которую надо прервать.</param>
        [Authorize]
        [HttpPost("revokeSession")]
        public async Task<IActionResult> RevokeSession(Guid sessionId)
        {
            await _authService.RevokeSessionAsync(sessionId);
            return NoContent();
        }

        /// <summary>
        /// Post запрос на разлогин всех сессий пользователя, кроме указанной.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="keepSessionId">Id сессии, которую нужно оставить.</param>
        [Authorize]
        [HttpPost("revokeOtherSessions")]
        public async Task<IActionResult> RevokeOtherSessions(Guid userId, Guid keepSessionId)
        {
            await _authService.RevokeOtherSessionsAsync(userId, keepSessionId);
            return NoContent();
        }

        /// <summary>
        /// Post запрос на разлогин всех сессий пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        [Authorize]
        [HttpPost("revokeAllSessions")]
        public async Task<IActionResult> RevokeAllSessions(Guid userId)
        {
            await _authService.RevokeAllSessionsAsync(userId);
            return NoContent();
        }
    }
}
