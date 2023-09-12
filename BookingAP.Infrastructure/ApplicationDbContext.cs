using BookingAP.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingAP.Infrastructure
{
    internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
