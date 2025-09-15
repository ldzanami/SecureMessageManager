using Microsoft.AspNetCore.Mvc;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Shared.DTOs.Auth;

namespace SecureMessageManager.Api.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за регистрацию и авторизацию пользователя в системе
    /// </summary>
    /// <param name="appDbContext"></param>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AppDbContext appDbContext) : ControllerBase
    {
        private AppDbContext AppDbContext { get; set; } = appDbContext;

        /// <summary>
        /// Post запрос для регистрации пользователя
        /// </summary>
        /// <param name="dto"> Dto в котором содержатся данные пользователя для регистрации </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            
        }
    }
}
