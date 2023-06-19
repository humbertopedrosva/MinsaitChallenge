using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.Security.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<string> AuthenticateAsync(LoginDTO loginDTO);
    }
}
