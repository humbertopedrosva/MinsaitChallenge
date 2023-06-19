using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.MerchantReleases.Entities;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;

namespace Minsait.Challenge.Domain.MerchantReleases.Services
{
    public class ReleaseUpdater : IMerchantReleaseUpdater
    {
        private readonly IMerchantReleaseRepository _releaseRepository;

        public ReleaseUpdater(IMerchantReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public async Task<Release?> UpdateAsync(ReleaseDTO updateReleaseDTO)
        {
            var release = await _releaseRepository.GetAsync(updateReleaseDTO.Id);

            if (release is null)
            {
                return default;
            }

            UpdatePropertiesAsync(updateReleaseDTO, release!);

            await _releaseRepository.UpdateAsync(release);

            return release;
        }

        private void UpdatePropertiesAsync(ReleaseDTO updateReleaseDTO, Release release)
        {
            release.Description = updateReleaseDTO.Description;
            release.TypeRelease = updateReleaseDTO.TypeRelease;
            release.Value = updateReleaseDTO.Value;
        }
    }
}