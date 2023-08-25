namespace BookingAP.Domain.Appartments.ValueObjects
{
    public record Address(string Country,
                          string State,
                          string ZipCode,
                          string City,
                          string Street);
}
