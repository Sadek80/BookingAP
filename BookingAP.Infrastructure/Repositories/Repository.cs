using BookingAP.Application.Abstractions.Repositories;
using BookingAP.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookingAP.Infrastructure.Repositories
{
    internal class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await  _dbContext.Set<T>().FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
