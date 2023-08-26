using BookingAP.Domain.Abstractions;
using ErrorOr;

namespace BookingAP.Domain.Reviews.ValueObjects
{
    public record Rating
    {
        private Rating(int value) => Value = value;

        public int Value { get; init; }

        public static ErrorOr<Rating> Create(int value)
        {
            if (value < 1 || value > 5)
            {
                return DomainError.Failure(DomainErrors.RatingErrors.Invalid);
            }

            return new Rating(value);
        }
    }
}
