using Minsait.Challenge.Domain.Interfaces;

namespace Minsait.Challenge.Domain
{
    public abstract class EntityBase : IEntity
    {
        public EntityBase() { }

        public Guid Id { get; set; }
    }
}
