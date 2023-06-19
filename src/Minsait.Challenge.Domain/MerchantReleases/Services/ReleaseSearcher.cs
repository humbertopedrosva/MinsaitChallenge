using Minsait.Challenge.Domain.MerchantReleases.Entities;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;

namespace Minsait.Challenge.Domain.MerchantReleases.Services
{
    public class ReleaseSearcher : IMerchantReleaseSearcher
    {
        private readonly IMerchantReleaseRepository _releaseRepository;

        public ReleaseSearcher(IMerchantReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public async Task<IEnumerable<Release>> GetAllFromMerchantAsync(Guid merchantId)
        {
            return await _releaseRepository.GetAllFromMerchantAsync(merchantId);
        }

        public Task<Release?> GetAsync(Guid id)
        {
            return id == Guid.Empty
                ? Task.FromResult(default(Release))
                : _releaseRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Release>> GetReleasesByPeriodFromMerchantAsync(Guid merchantId, DateTime beginDate, DateTime endDate)
        {
            return await _releaseRepository.GetConsolidateByPeriodFromMerchantAsync(merchantId, beginDate, endDate);
        }
    }
}

