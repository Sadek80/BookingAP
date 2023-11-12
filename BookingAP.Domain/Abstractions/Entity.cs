namespace BookingAP.Domain.Abstractions
{
    public abstract class Entity<TEntityId> : IEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(TEntityId id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public TEntityId Id { get; init; }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        protected void RaisDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
