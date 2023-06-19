using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Merchants.Entities;

namespace Minsait.Challenge.Domain.Merchants.Interfaces
{
    public interface IMerchantCreator
    {
        Task<Merchant> CreateAsync(MerchantDTO createMerchantDTO);
    }
}
