using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minsait.Challenge.Domain.MerchantReleases.Entities;

namespace Minsait.Challenge.Infra.Mappings
{
    public class MerchantReleaseMapping : IEntityTypeConfiguration<Release>
    {
        public void Configure(EntityTypeBuilder<Release> builder)
        {
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.TypeRelease).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.MerchantId);

            builder.HasOne(x => x.Merchant)
                   .WithMany(x => x.Releases)
                   .HasForeignKey(x => x.MerchantId);
        }
    }
}
