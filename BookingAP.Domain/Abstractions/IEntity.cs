namespace BookingAP.Domain.Abstractions
{
    public interface IEntity
    {
        public Guid Id { get; init; }

        IReadOnlyList<IDomainEvent> GetDomainEvents();
        void ClearDomainEvents();
    }
}
