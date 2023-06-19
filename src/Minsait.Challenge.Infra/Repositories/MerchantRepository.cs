using Microsoft.EntityFrameworkCore;
using Minsait.Challenge.Domain.Merchants.Entities;
using Minsait.Challenge.Domain.Merchants.Interfaces;

namespace Minsait.Challenge.Infra.Repositories
{
    public class MerchantRepository : RepositoryBase<Merchant>, IMerchantRepository
    {
        public MerchantRepository(MerchantContext merchantContext) : base(merchantContext)
        {
        }

        public Task<Merchant?> GetForLoginAsync(string email, string passwordHashed)
        {
            return Set.Where(x => x.Email == email && x.PasswordHash == passwordHashed).FirstOrDefaultAsync();
        }
    }
}
