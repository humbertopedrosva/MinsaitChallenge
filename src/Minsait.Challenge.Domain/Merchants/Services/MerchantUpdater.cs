using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Merchants.Entities;
using Minsait.Challenge.Domain.Merchants.Interfaces;

namespace Minsait.Challenge.Domain.Merchants.Services
{
    public class MerchantUpdater : IMerchantUpdater
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantUpdater(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<Merchant?> UpdateAsync(MerchantDTO updateMerchantDTO)
        {
            var merchant = await _merchantRepository.GetAsync(updateMerchantDTO.Id);
            if (merchant == null)
            {
                return null;
            }

            UpdatePropertiesAsync(updateMerchantDTO, merchant!);

            await _merchantRepository.UpdateAsync(merchant!);

            return merchant!;
        }

        private void UpdatePropertiesAsync(MerchantDTO updateMerchantDTO, Merchant merchant)
        {
            merchant.Name = updateMerchantDTO.Name;
            merchant.Surname = updateMerchantDTO.Surname;
            merchant.Email = updateMerchantDTO.Email;
        }
    }
}
