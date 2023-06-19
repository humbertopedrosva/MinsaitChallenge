using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.Application.Services.Security
{
    public interface ILoginFacade
    {
        Task<string> AuthenticateAsync(LoginDTO loginDTO);
    }
}
