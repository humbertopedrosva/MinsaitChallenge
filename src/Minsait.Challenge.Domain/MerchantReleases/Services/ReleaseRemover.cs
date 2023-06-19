using Minsait.Challenge.Domain.MerchantReleases.Interfaces;

namespace Minsait.Challenge.Domain.MerchantReleases.Services
{
    public class ReleaseRemover : IMerchantReleaseRemover
    {
        private readonly IMerchantReleaseRepository _releaseRepository;

        public ReleaseRemover(IMerchantReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public async Task DeleteAllFromMerchant(Guid merchantId)
        {
            var releases = await _releaseRepository.GetAllFromMerchantAsync(merchantId);
            foreach (var release in releases)
            {
                await _releaseRepository.DeleteAsync(release);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _releaseRepository.GetAsync(id);
            if (entity != null)
            {
                await _releaseRepository.DeleteAsync(entity);
            }
        }
    }
}
