using Minsait.Challenge.Domain.DTOs;

namespace Minsait.Challenge.Application.Services.MerchantReleases
{
    public interface IMerchantReleaseFacade
    {
        Task<ReleaseDTO> CreateAsync(ReleaseDTO createMerchantReleaseDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ReleaseDTO>> GetAllFromMerchant(Guid merchantId);
        Task<ConsolidateReleasesDTO> GetConsolidateByPeriodFromMerchantAsync(Guid merchantId, DateTime beginDate, DateTime endDate);
        Task<ReleaseDTO?> GetAsync(Guid id);
        Task<ReleaseDTO?> UpdateAsync(ReleaseDTO updateMerchantReleaseDTO);
        Task DeleteAllFromMerchant(Guid merchantId);
    }
}
