using Minsait.Challenge.Domain.DTOs;
using Minsait.Challenge.Domain.Merchants.Entities;

namespace Minsait.Challenge.Domain.Mappings
{
    public static class MerchantMappings
    {
        public static MerchantDTO ToDto(this Merchant employee)
        {
            return new MerchantDTO
            {
                Email = employee.Email,
                Name = employee.Name,
                Surname = employee.Surname,
                Id = employee.Id
            };
        }
    }
}
