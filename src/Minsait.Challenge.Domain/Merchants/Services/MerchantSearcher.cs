using Minsait.Challenge.Domain.Merchants.Entities;
using Minsait.Challenge.Domain.Merchants.Interfaces;

namespace Minsait.Challenge.Domain.Merchants.Services
{
    public class MerchantSearcher : IMerchantSearcher
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantSearcher(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public Task<IEnumerable<Merchant>> GetAsync()
        {
            return _merchantRepository.GetAsync();
        }

        public Task<Merchant?> GetAsync(Guid id)
        {
            return id == Guid.Empty
                ? Task.FromResult(default(Merchant))
                : _merchantRepository.GetAsync(id);
        }

        public Task<Merchant?> GetForLoginAsync(string email, string passwordHashed)
        {
            return _merchantRepository.GetForLoginAsync(email, passwordHashed);
        }
    }
}
