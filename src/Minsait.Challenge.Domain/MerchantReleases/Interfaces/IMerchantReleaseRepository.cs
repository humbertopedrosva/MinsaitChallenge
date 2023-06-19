using Minsait.Challenge.Domain.Interfaces;
using Minsait.Challenge.Domain.MerchantReleases.Entities;

namespace Minsait.Challenge.Domain.MerchantReleases.Interfaces
{
    public interface IMerchantReleaseRepository : IRepository<Release>
    {
        Task<IEnumerable<Release>> GetAllFromMerchantAsync(Guid merchantId);
        Task<IEnumerable<Release>> GetConsolidateByPeriodFromMerchantAsync(Guid merchantId, DateTime beginDate, DateTime endDate);
    }
}
