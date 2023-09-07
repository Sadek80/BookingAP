using BookingAP.Application.Abstractions.Services;
using BookingAP.Domain.Users.ValueObjects;

namespace BookingAP.Infrastructure.Services
{
    internal sealed class EmailService : IEmailService
    {
        public async Task SendAsync(Email email, string subject, string body)
        {
            await Task.CompletedTask;
        }
    }
}
