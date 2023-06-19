using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Merchants.Entities;

namespace Minsait.Challenge.Domain.Merchants.Interfaces
{
    public interface IMerchantUpdater
    {
        Task<Merchant?> UpdateAsync(MerchantDTO updateMerchantDTO);
    }
}
