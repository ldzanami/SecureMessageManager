using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureMessageManager.Api.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за работу с участниками чата.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MembersController : ControllerBase
    {

    }
}
