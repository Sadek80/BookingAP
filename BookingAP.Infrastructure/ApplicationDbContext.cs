using BookingAP.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingAP.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
