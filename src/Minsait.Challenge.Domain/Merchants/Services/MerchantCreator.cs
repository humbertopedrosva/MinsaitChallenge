using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Merchants.Entities;
using Minsait.Challenge.Domain.Merchants.Interfaces;

namespace Minsait.Challenge.Domain.Merchants.Services
{
    public class MerchantCreator : IMerchantCreator
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantCreator(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<Merchant> CreateAsync(MerchantDTO createMerchantDTO)
        {
            var Merchant = new Merchant(
                createMerchantDTO.Name,
                createMerchantDTO.Surname,
                createMerchantDTO.Email,
                createMerchantDTO.Name + createMerchantDTO.Surname
            );

            return await _merchantRepository.CreateAsync(Merchant);
        }
    }
}
