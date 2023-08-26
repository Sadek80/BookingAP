using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Application.Abstractions.Repositories.Appartments;
using BookingAP.Domain.Bookings.Enums;
using ErrorOr;

namespace Bookify.Application.Apartments.SearchApartments;

internal sealed class SearchApartmentsQueryHandler
    : IQueryHandler<SearchApartmentsQuery, ErrorOr<IReadOnlyList<ApartmentResponse>>>
{
    private static readonly int[] ActiveBookingStatuses =
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed
    };

    private readonly IAppartmentRepository _appartmentRepository;

    public SearchApartmentsQueryHandler(IAppartmentRepository appartmentRepository)
    {
        _appartmentRepository = appartmentRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<ApartmentResponse>();
        }

        var appartments = await _appartmentRepository.Search<ApartmentResponse>(request.StartDate, request.StartDate, ActiveBookingStatuses, cancellationToken);

        return appartments.ToList();
    }
}