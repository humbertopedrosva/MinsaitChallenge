using Microsoft.EntityFrameworkCore;
using Minsait.Challenge.Domain.MerchantReleases.Entities;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;

namespace Minsait.Challenge.Infra.Repositories
{
    public class MerchantReleaseRepository : RepositoryBase<Release>, IMerchantReleaseRepository
    {
        public MerchantReleaseRepository(MerchantContext merchantContext) : base(merchantContext)
        {
        }

        public async Task<IEnumerable<Release>> GetAllFromMerchantAsync(Guid merchantId)
        {
            return await Set.Where(x => x.MerchantId == merchantId).ToListAsync();
        }

        public async Task<IEnumerable<Release>> GetConsolidateByPeriodFromMerchantAsync(Guid merchantId, DateTime beginDate, DateTime endDate)
        {
            return await Set.Where(x => x.MerchantId == merchantId && x.Date >= beginDate && x.Date <= endDate).ToListAsync();
        }
    }
}
