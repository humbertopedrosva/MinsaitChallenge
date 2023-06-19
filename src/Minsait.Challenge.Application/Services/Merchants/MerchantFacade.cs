using Minsait.Challenge.Application.Services.MerchantReleases;
using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Mappings;
using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Security.Domain.DTOs;
using Minsait.Challenge.Security.Domain.Interfaces;

namespace Minsait.Challenge.Application.Services.Merchants
{
    public class MerchantFacade : IMerchantFacade
    {
        private readonly IMerchantCreator _merchantCreator;
        private readonly IMerchantUpdater _merchantUpdater;
        private readonly IMerchantRemover _merchantRemover;
        private readonly IMerchantSearcher _merchantSearcher;
        private readonly IMerchantReleaseFacade _merchantReleaseFacade;
        private readonly IPasswordHasher _passwordHasher;

        public MerchantFacade
        (
            IMerchantCreator merchantCreator,
            IMerchantUpdater merchantUpdater,
            IMerchantRemover merchantRemover,
            IMerchantSearcher merchantSearcher,
            IMerchantReleaseFacade merchantReleaseFacade,
            IPasswordHasher passwordHasher
        )
        {
            _merchantCreator = merchantCreator;
            _merchantUpdater = merchantUpdater;
            _merchantRemover = merchantRemover;
            _merchantSearcher = merchantSearcher;
            _merchantReleaseFacade = merchantReleaseFacade;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<MerchantDTO>> GetAsync()
        {
            var merchants = await _merchantSearcher.GetAsync();
            return merchants.Select(x => x.ToDto());

        }

        public async Task<MerchantDTO?> GetAsync(Guid id)
        {
            var merchant = await _merchantSearcher.GetAsync(id);
            return merchant?.ToDto();
        }

        public async Task<MerchantDTO> CreateAsync(MerchantDTO createEmployeeDTO)
        {
            var merchant = await _merchantCreator.CreateAsync(createEmployeeDTO);
            return merchant.ToDto();
        }

        public async Task<MerchantDTO?> UpdateAsync(MerchantDTO updateEmployeeDTO)
        {
            var merchant = await _merchantUpdater.UpdateAsync(updateEmployeeDTO);
            return merchant?.ToDto();
        }

        public async Task DeleteAsync(Guid id)
        {
           await _merchantReleaseFacade.DeleteAllFromMerchant(id);

            await _merchantRemover.DeleteAsync(id);
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        {
            var merchant = await _merchantSearcher.GetForLoginAsync(changePasswordDTO.Email, _passwordHasher.HashPassword(changePasswordDTO.OldPassword));
            if (merchant != null)
            {
                merchant.UpdatePassword(_passwordHasher.HashPassword(changePasswordDTO.NewPassword));
            }
        }

        public async Task CreatePasswordAsync(CreatePasswordDTO createPasswordDTO)
        {
            var employee = await _merchantSearcher.GetForLoginAsync(createPasswordDTO.Email, string.Empty);
            if (employee != null)
            {
                employee.UpdatePassword(_passwordHasher.HashPassword(createPasswordDTO.Password));
            }
        }
    }
}
