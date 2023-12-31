﻿using BookingAp.Contract.Users;
using BookingAP.Application.Abstractions.Clock;
using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Abstractions;
using BookingAP.Domain.Appartments.Repositories;
using BookingAP.Domain.Appartments.ValueObjects;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Bookings.Repositories;
using BookingAP.Domain.Bookings.Services;
using BookingAP.Domain.Bookings.ValueObjects;
using BookingAP.Domain.Exceptions;
using BookingAP.Domain.Users.Repositories;
using BookingAP.Domain.Users.ValueObjects;
using ErrorOr;
using static BookingAP.Domain.Appartments.DomainErrors;
using static BookingAP.Domain.Bookings.DomainErrors;
using static BookingAP.Domain.Users.DomainErrors;

namespace BookingAP.Application.Bookings.ReserveBooking
{
    internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, ErrorOr<BookingId>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppartmentRepository _appartmentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly BookingPricingService _bookingPricingService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public ReserveBookingCommandHandler(IUserRepository userRepository,
                                            IAppartmentRepository appartmentRepository,
                                            IBookingRepository bookingRepository,
                                            BookingPricingService bookingPricingService,
                                            IDateTimeProvider dateTimeProvider,
                                            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _appartmentRepository = appartmentRepository;
            _bookingRepository = bookingRepository;
            _bookingPricingService = bookingPricingService;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BookingId>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
        {
            var userIdDto = await _userRepository.GetUserByIdentityIdAsync<UserIdDto>(request.userIdentityId, cancellationToken);

            if (userIdDto is null)
            {
                return DomainError.NotFound(UserErrors.NotFound);
            }

            var appartment = await _appartmentRepository.GetByIdAsync(new AppartmentId(request.appartmentId), cancellationToken);

            if (appartment is null)
            {
                return DomainError.NotFound(AppartmentErrors.NotFound);
            }

            var duration = DateRange.Create(request.startDate, request.endDate);

            if (await _bookingRepository.IsOverlappingAsync(appartment, duration, cancellationToken))
            {
                return DomainError.Conflict(BookingErrors.Overlap);
            }

            try
            {
                var booking = Booking.Reserve(new UserId(userIdDto.Id),
                                         appartment,
                                         duration,
                                         _bookingPricingService,
                                         _dateTimeProvider.UTCNow);

                _bookingRepository.Add(booking);

                await _unitOfWork.SaveChangesAsync();

                return booking.Id;
            }
            catch (ConcurrencyException)
            {
                return DomainError.Conflict(BookingErrors.Overlap);
            }
        }
    }
}
