using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.MerchantReleases.Entities;

namespace Minsait.Challenge.Domain.MerchantReleases.Interfaces
{
    public interface IMerchantReleaseCreator
    {
        Task<Release> CreateAsync(ReleaseDTO releaseDTO);
    }
}
