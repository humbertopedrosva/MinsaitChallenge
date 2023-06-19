using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.MerchantReleases.Entities;

namespace Minsait.Challenge.Domain.MerchantReleases.Interfaces
{
    public interface IMerchantReleaseUpdater
    {
        Task<Release?> UpdateAsync(ReleaseDTO updatereleaseDTO);
    }
}
