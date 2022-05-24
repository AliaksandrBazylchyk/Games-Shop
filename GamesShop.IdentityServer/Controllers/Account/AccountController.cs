using AutoMapper;
using GamesShop.Common;
using GamesShop.IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesShop.IdentityServer.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<UserEntity> userManager,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for registering a new ASP.NET Identity user and saving the result to the database
        /// <param name="model">Model containing email address, username and password for user identification</param>
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            var user = _mapper.Map<RegisterUserModel, UserEntity>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.UserRole);
            }

            return Ok(result);
        }
    }
}