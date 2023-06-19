using Minsait.Challenge.Domain.MerchantReleases.Entities;
using System.Collections.ObjectModel;

namespace Minsait.Challenge.Domain.Merchants.Entities
{
    public class Merchant : EntityBase
    {
        public Merchant() { }

        public Merchant(string name, string surname, string email, string passwordHash)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PasswordHash = passwordHash ?? string.Empty;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Release> Releases { get; set; } = new Collection<Release>();

        public void UpdatePassword(string passwordHashed)
        {
            PasswordHash = passwordHashed;
        }
    }
}
