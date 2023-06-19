using Minsait.Challenge.Domain.MerchantReleases.Entities;

namespace Minsait.Challenge.Domain.MerchantReleases.Interfaces
{
    public interface IMerchantReleaseSearcher
    {
        Task<Release?> GetAsync(Guid id);
        Task<IEnumerable<Release>> GetAllFromMerchantAsync(Guid merchantId);
        Task<IEnumerable<Release>> GetReleasesByPeriodFromMerchantAsync(Guid merchantId, DateTime beginDate, DateTime endDate);
    }
}
