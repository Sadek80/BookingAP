namespace BookingAP.Domain.Abstractions
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> GetDomainEvents();
        void ClearDomainEvents();
    }
}
