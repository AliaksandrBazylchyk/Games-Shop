using GamesShop.UserManagement.API.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GamesShop.UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel model)
        {
            return Ok();
        }
    }
}
