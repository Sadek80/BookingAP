using BookingAP.Domain.Appartments;

namespace BookingAP.Application.Abstractions.Repositories.Appartments
{
    public interface IAppartmentRepository : IRepository<Appartment>
    {
        Task<IReadOnlyList<TResult>> Search<TResult>(DateOnly startDate, DateOnly endDate, int[] activeBookingStatuses, CancellationToken cancellationToken = default);
    }
}
