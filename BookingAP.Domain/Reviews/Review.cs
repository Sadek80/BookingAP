using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Bookings.Enums;
using BookingAP.Domain.Reviews.Events;
using BookingAP.Domain.Reviews.ValueObjects;
using ErrorOr;

namespace BookingAP.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review(Guid Id,
                      Guid userId,
                      Guid appartmentId,
                      Guid bookingId,
                      Comment comment,
                      Rating rating,
                      DateTime createdOnUTC)
            :base(Id)
        {
            UserId = userId;
            AppartmentId = appartmentId;
            BookingId = bookingId;
            Comment = comment;
            Rating = rating;
            CreatedOnUTC = createdOnUTC;
        }

        public Guid UserId { get; private set; }
        public Guid AppartmentId { get; private set; }
        public Guid BookingId { get; private set; }
        public Comment Comment { get; private set; }
        public Rating Rating { get; internal set; }
        public DateTime CreatedOnUTC { get; private set; }

        public static ErrorOr<Review> Create(Booking booking,
                                             Rating rating,
                                             Comment comment,
                                             DateTime createdOnUtc)
        {
            if(booking.Status != BookingStatus.Completed)
            {
                return DomainError.Failure(DomainErrors.ReviewErrors.NotEligible);
            }

            var review = new Review(Guid.NewGuid(),
                                    booking.UserId,
                                    booking.AppartmentId,
                                    booking.Id,
                                    comment,
                                    rating,
                                    createdOnUtc);

            review.RaisDomainEvent(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }
    }
}
