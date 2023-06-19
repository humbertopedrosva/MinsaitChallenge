using Minsait.Challenge.Domain.Enums;
using Minsait.Challenge.Domain.Merchants.Entities;
using System;

namespace Minsait.Challenge.Domain.MerchantReleases.Entities
{
    public class Release : EntityBase
    {
        public Release() { }

        public Release(string description, TypeRelease typeRelease, decimal value, Guid merchantId, DateTime dateTime)
        {
            Description = description;
            TypeRelease = typeRelease;
            Value = value;
            MerchantId = merchantId;
            Date = dateTime;
        }

        public string Description { get; set; }
        public TypeRelease TypeRelease { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }
    }
}
