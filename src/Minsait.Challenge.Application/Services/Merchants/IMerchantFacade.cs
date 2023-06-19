using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Security.Domain.DTOs;

namespace Minsait.Challenge.Application.Services.Merchants
{
    public interface IMerchantFacade
    {
        Task<MerchantDTO> CreateAsync(MerchantDTO createEmployeeDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MerchantDTO>> GetAsync();
        Task<MerchantDTO?> GetAsync(Guid id);
        Task<MerchantDTO?> UpdateAsync(MerchantDTO updateEmployeeDTO);
        Task ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
        Task CreatePasswordAsync(CreatePasswordDTO createPasswordDTO);
    }
}
