using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.MerchantReleases.Entities;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;

namespace Minsait.Challenge.Domain.MerchantReleases.Services
{
    public class ReleaseCreator : IMerchantReleaseCreator
    {
        private readonly IMerchantReleaseRepository _releaseRepository;

        public ReleaseCreator(IMerchantReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public async Task<Release> CreateAsync(ReleaseDTO createReleaseDTO)
        {
            var release = new Release(
                createReleaseDTO.Description, 
                createReleaseDTO.TypeRelease,
                createReleaseDTO.Value,
                createReleaseDTO.MerchantId,
                DateTime.UtcNow);

            return await _releaseRepository.CreateAsync(release);
        }
    }
}
