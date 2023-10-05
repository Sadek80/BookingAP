using BookingAP.Application.Abstractions.Services;
using BookingAP.Domain.Bookings.Events;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Users.Repositories;
using MediatR;

namespace BookingAP.Application.Bookings.ConfirmBooking
{
    internal sealed class BookingConfirmedDomainEventHandler : INotificationHandler<BookingConfirmedDomainEvent>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public BookingConfirmedDomainEventHandler(IBookingRepository bookingRepository,
                                                 IUserRepository userRepository,
                                                 IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task Handle(BookingConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

            if (booking is null)
            {
                return;
            }

            var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);

            if (user is null) { return; }

            await _emailService.SendAsync(user.Email,
                                          "Booking Confirmed!",
                                          "You have Successfully confirm this booking");
        }
    }
}
