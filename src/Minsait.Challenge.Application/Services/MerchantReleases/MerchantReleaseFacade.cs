using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Mappings;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;

namespace Minsait.Challenge.Application.Services.MerchantReleases
{
    public class MerchantReleaseFacade : IMerchantReleaseFacade
    {
        private readonly IMerchantReleaseCreator _releaseCreator;
        private readonly IMerchantReleaseUpdater _releaseUpdater;
        private readonly IMerchantReleaseRemover _releaseRemover;
        private readonly IMerchantReleaseSearcher _releaseSearcher;

        public MerchantReleaseFacade
        (
            IMerchantReleaseCreator releaseCreator,
            IMerchantReleaseUpdater releaseUpdater,
            IMerchantReleaseRemover releaseRemover,
            IMerchantReleaseSearcher releaseSearcher
        )
        {
            _releaseCreator = releaseCreator;
            _releaseUpdater = releaseUpdater;
            _releaseRemover = releaseRemover;
            _releaseSearcher = releaseSearcher;
        }

        public async Task<IEnumerable<ReleaseDTO>> GetAllFromMerchant(Guid merchantId)
        {
            var releases = await _releaseSearcher.GetAllFromMerchantAsync(merchantId);
            return releases.Select(x => x.ToDto());
        }

        public async Task<ReleaseDTO?> GetAsync(Guid id)
        {
            var release = await _releaseSearcher.GetAsync(id);
            return release?.ToDto();
        }

        public async Task<ReleaseDTO> CreateAsync(ReleaseDTO createMerchantReleaseDTO)
        {
             var release = await _releaseCreator.CreateAsync(createMerchantReleaseDTO);
            return release.ToDto();
        }

        public async Task<ReleaseDTO?> UpdateAsync(ReleaseDTO updateMerchantReleaseDTO)
        {
            var release = await _releaseUpdater.UpdateAsync(updateMerchantReleaseDTO);
            return release?.ToDto();
        }

        public Task DeleteAsync(Guid id)
        {
            return _releaseRemover.DeleteAsync(id);
        }

        public Task DeleteAllFromMerchant(Guid merchantId)
        {
            return _releaseRemover.DeleteAllFromMerchant(merchantId);
        }

        public async Task<ConsolidateReleasesDTO> GetConsolidateByPeriodFromMerchantAsync(Guid merchantId, DateTime beginDate, DateTime endDate)
        {
            var result = new ConsolidateReleasesDTO();

            var releases = await _releaseSearcher.GetReleasesByPeriodFromMerchantAsync(merchantId, beginDate, endDate);

            foreach (var release in releases)
                result.Releases.Add(release.ToDto());

            result.beginDate = beginDate;
            result.endDate = endDate;

            result.Balance = result.Releases.Sum(x => x.Value);

            return result;
        }
    }
}
