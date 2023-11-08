using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Users.ValueObjects;

namespace BookingAP.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
}
