namespace BookingAP.Domain.Abstractions
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(T entity, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    }
}
