namespace BookingAP.Domain.Abstractions
{
    public abstract class Entity : IEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; init; }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents;
        }

        protected void RaisDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
