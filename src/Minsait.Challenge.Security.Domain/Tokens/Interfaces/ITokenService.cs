using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.Security.Domain.Tokens.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(UserDTO user, IEnumerable<string> roles);
    }
}
