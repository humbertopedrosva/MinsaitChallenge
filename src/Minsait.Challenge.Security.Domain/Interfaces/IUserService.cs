using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.Security.Domain.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserAsync(string email, string passwordHashed);
    }
}
