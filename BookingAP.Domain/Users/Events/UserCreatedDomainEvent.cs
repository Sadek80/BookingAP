using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
}
