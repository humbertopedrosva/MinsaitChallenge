﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minsait.Challenge.Application.Services.Security;
using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILoginFacade _loginFacade;

        public LoginController(ILoginFacade loginFacade)
        {
            _loginFacade = loginFacade;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
        {
            var token = await _loginFacade.AuthenticateAsync(loginDTO);

            return string.IsNullOrWhiteSpace(token)
                        ? Unauthorized()
                        : Ok(new { token });
        }
    }
}
