using Minsait.Challenge.Domain.Merchants.Interfaces;

namespace Minsait.Challenge.Domain.Merchants.Services
{
    public class MerchantRemover : IMerchantRemover
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantRemover(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task DeleteAsync(Guid id)
        {
            var merchant = await _merchantRepository.GetAsync(id);

            if (merchant != null)
            {
                await _merchantRepository.DeleteAsync(merchant);
            }
        }
    }
}
