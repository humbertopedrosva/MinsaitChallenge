using Minsait.Challenge.Security.Domain.DTOs;
using Minsait.Challenge.Security.Domain.Interfaces;
using Minsait.Challenge.Security.Domain.Tokens.Interfaces;

namespace Minsait.Challenge.Security.Domain
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginService(ITokenService tokenService, IUserService userService, IPasswordHasher passwordHasher)
        {
            _tokenService = tokenService;
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> AuthenticateAsync(LoginDTO loginDTO)
        {
            var user = await _userService.GetUserAsync(loginDTO.Email, _passwordHasher.HashPassword(loginDTO.Password));

            return user == null ? string.Empty
                                : _tokenService.BuildToken(user, Enumerable.Empty<string>());
        }
    }
}
