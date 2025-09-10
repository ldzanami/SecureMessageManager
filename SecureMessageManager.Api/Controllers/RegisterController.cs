using Microsoft.AspNetCore.Mvc;
using SecureMessageManager.Api.Data;
using SecureMessageManager.Shared.DTOs;

namespace SecureMessageManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController(AppDbContext appDbContext) : ControllerBase
    {
        private AppDbContext AppDbContext { get; set; } = appDbContext;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto)
        {

        }
    }
}
