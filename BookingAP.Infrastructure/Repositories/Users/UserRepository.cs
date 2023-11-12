using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookingAP.Domain.Users;
using BookingAP.Domain.Users.Abstractions;
using BookingAP.Domain.Users.Repositories;
using BookingAP.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BookingAP.Infrastructure.Repositories.Users
{
    internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TResult?> GetUserByIdentityIdAsync<TResult>(string identityId, CancellationToken cancellationToken = default) 
            where TResult : IUserResponse
        {
            var user = await _dbContext.Set<User>()
                                       .AsNoTracking()
                                       .Where(f => f.IdentityId == identityId)
                                       .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                                       .FirstOrDefaultAsync(cancellationToken);

            return user;
        }
    }
}
