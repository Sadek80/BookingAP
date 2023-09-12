using BookingAP.Domain.Abstractions;

namespace BookingAP.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    }
}
