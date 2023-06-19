using Microsoft.EntityFrameworkCore;
using Minsait.Challenge.Infra.Mappings;

namespace Minsait.Challenge.Infra
{
    public class MerchantContext : DbContext
    {
        protected MerchantContext() { }

        public MerchantContext(DbContextOptions<MerchantContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.ApplyConfiguration(new MerchantMapping());
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
