using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments.ValueObjects;

namespace BookingAP.Domain.Appartments.Repositories
{
    public interface IAppartmentRepository : IRepository<Appartment, AppartmentId>
    {
    }
}
