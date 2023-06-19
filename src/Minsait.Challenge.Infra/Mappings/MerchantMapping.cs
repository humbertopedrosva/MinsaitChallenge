using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minsait.Challenge.Domain.Merchants.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Minsait.Challenge.Infra.Mappings
{
    public class MerchantMapping : IEntityTypeConfiguration<Merchant>
    {
        private const string AdminId = "661b8028-6ce0-4544-950d-18837c2bcd7e";
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Surname).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PasswordHash);

            builder.HasMany(x => x.Releases)
                   .WithOne(x => x.Merchant)
                   .HasForeignKey(x => x.MerchantId);

            builder.HasData(new Merchant("Admin", string.Empty, "admin@admin.com", GetPassword())
            {
                Id = Guid.Parse(AdminId)
            });
        }

        private static string GetPassword()
        {
            return SHA256.Create()
                         .ComputeHash(Encoding.UTF8.GetBytes("admin"))
                         .Select(x => string.Format("{0:x2}", x))
                         .Aggregate((@byte, hash) => hash + @byte);
        }
    }
}
