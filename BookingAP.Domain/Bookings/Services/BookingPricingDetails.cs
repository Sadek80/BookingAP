using BookingAP.Domain.Shared.ValueObjects;

namespace BookingAP.Domain.Bookings.Services
{
    public record BookingPricingDetails(Money PriceForPeriod,
                                        Money CleaningFee,
                                        Money AmenitiesUpCharge,
                                        Money TotalPrice);
}
