using BookingAp.Contract.Appartments;
using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;

namespace BookingAP.Application.Apartments.SearchApartments;

public sealed record SearchApartmentsQuery(DateOnly StartDate,
                                           DateOnly EndDate) : IQuery<ErrorOr<IReadOnlyList<ApartmentResponse>>>;