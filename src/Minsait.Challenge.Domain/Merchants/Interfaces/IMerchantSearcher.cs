using Minsait.Challenge.Domain.Merchants.Entities;

namespace Minsait.Challenge.Domain.Merchants.Interfaces
{
    public interface IMerchantSearcher
    {
        Task<IEnumerable<Merchant>> GetAsync();
        Task<Merchant?> GetAsync(Guid id);
        Task<Merchant?> GetForLoginAsync(string email, string passwordHashed);
    }
}
