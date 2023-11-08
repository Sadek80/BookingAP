using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Reviews.ValueObjects
{
    public record ReviewId(Guid Value) : IEntityId<ReviewId>
    {
        public static ReviewId New => new ReviewId(Guid.NewGuid());
    }
}
