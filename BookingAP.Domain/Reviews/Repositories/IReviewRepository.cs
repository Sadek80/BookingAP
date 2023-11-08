using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Reviews.ValueObjects;

namespace BookingAP.Domain.Reviews.Repositories
{
    public interface IReviewRepository : IRepository<Review, ReviewId>
    {
    }
}
