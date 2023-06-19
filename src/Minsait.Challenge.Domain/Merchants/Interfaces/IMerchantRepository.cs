using Minsait.Challenge.Domain.Interfaces;
using Minsait.Challenge.Domain.Merchants.Entities;

namespace Minsait.Challenge.Domain.Merchants.Interfaces
{
    public interface IMerchantRepository : IRepository<Merchant>
    {
        Task<Merchant?> GetForLoginAsync(string email, string passwordHashed);
    }
}
