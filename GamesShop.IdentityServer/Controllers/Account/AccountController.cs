using AutoMapper;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using GamesShop.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesShop.IdentityServer.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events, 
            IMapper mapper, 
            RoleManager<RoleEntity> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            var user = _mapper.Map<RegisterUserModel, UserEntity>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // await _userManager.AddToRoleAsync(user, "User");
            }

            return Ok(result);

        }
    }
}