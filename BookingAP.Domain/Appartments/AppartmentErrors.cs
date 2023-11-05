using BookingAP.Domain.Abstractions;

namespace BookingAP.Domain.Appartments
{
    public static partial class DomainErrors
    {
        public static class AppartmentErrors
        {
            public static DomainError NotFound = new(
              "Appartment.NotFound",
              "The appartment with the specified identifier was not found");
        }
    }
    
}
