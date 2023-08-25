using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments.Enums;
using BookingAP.Domain.Appartments.ValueObjects;
using BookingAP.Domain.Shared.ValueObjects;

namespace BookingAP.Domain.Appartments
{
    public sealed class Appartment : Entity
    {
        public Appartment(Guid Id, 
                          Name name,
                          Address address,
                          Description description,
                          Money price,
                          Money cleaningFee,
                          List<Amenity> amenities,
                          DateTime? lastBookedOnUTC)
            :base(Id)
        {
            Name = name;
            Address = address;
            Description = description;
            Price = price;
            CleaningFee = cleaningFee;
            Amenities = amenities;
            LastBookedOnUTC = lastBookedOnUTC;
        }

        public Name Name { get; private set; }
        public Address Address { get; private set; }
        public Description Description { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public List<Amenity> Amenities { get; private set; } = new();
        public DateTime? LastBookedOnUTC { get; internal set; }
    }
}
