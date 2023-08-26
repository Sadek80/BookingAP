using BookingAP.Domain.Users.ValueObjects;

namespace BookingAP.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendAsync(Email email, string subject, string body);
    }
}
