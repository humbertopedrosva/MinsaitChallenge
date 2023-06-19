using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Security.Domain.DTOs;
using Minsait.Challenge.Security.Domain.Interfaces;

namespace Minsait.Challenge.Application.Services.Security
{
    public class UserService : IUserService
    {
        private readonly IMerchantSearcher _merchantSearcher;

        public UserService(IMerchantSearcher employeeSearcher)
        {
            _merchantSearcher = employeeSearcher;
        }

        public async Task<UserDTO?> GetUserAsync(string email, string passwordHashed)
        {
            var merchant = await _merchantSearcher.GetForLoginAsync(email, passwordHashed);

            if (merchant == null)
            {
                return default;
            }

            return new()
            {
                Id = merchant.Id,
                Name = merchant.Name,
                Surname = merchant.Surname,
                Email = merchant.Email
            };
        }
    }
}
