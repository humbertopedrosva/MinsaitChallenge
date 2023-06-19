using Minsait.Challenge.Security.Domain.DTOs;
using Minsait.Challenge.Security.Domain.Interfaces;

namespace Minsait.Challenge.Application.Services.Security
{
    public class LoginFacade : ILoginFacade
    {
        private readonly ILoginService _loginService;

        public LoginFacade(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public Task<string> AuthenticateAsync(LoginDTO loginDTO)
        {
            return _loginService.AuthenticateAsync(loginDTO);
        }
    }
}
