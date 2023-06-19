using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.MerchantReleases.Entities;

namespace Minsait.Challenge.Domain.Mappings
{
    public static class ReleaseMappings
    {
        public static ReleaseDTO ToDto(this Release release)
        {
            return new ReleaseDTO
            {
                Id = release.Id,
                MerchantId = release.MerchantId,
                Description = release.Description,
                TypeRelease = release.TypeRelease,
                Value = release.Value,
                Date = release.Date,
            };
        }
    }
}
