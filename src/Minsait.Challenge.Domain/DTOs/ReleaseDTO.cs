using Minsait.Challenge.Domain.Enums;

namespace Minsait.Challenge.Domain.DTOs
{
    public class ReleaseDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public TypeRelease TypeRelease { get; set; }
        public decimal Value { get; set; }
        public Guid MerchantId { get; set; }
        public DateTime Date { get; set; }
    }
}
