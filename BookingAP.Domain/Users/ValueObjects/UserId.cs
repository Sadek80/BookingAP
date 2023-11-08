using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Users.ValueObjects
{
    public record UserId(Guid Value) : IEntityId<UserId>
    {
        public static UserId New => new UserId(Guid.NewGuid());
    }
}
