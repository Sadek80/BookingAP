﻿using BookingAp.Contract.Bookings;
using BookingAP.Application.Abstractions.Authentication;
using BookingAP.Application.Bookings.GetBooking;
using BookingAP.Application.Bookings.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingAp.API.Controllers.Bookings;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ApiController
{
    private readonly ISender _sender;
    private readonly IUserContext _userContext;

    public BookingsController(ISender sender, IUserContext userContext)
    {
        _sender = sender;
        _userContext = userContext;
    }

    [HttpGet("{id}", Name = "GetBooking")]
    public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.Match(success => Ok(result.Value),
                            Problem);
    }

    [HttpPost]
    public async Task<IActionResult> ReserveBooking(
        ReserveBookingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(
            _userContext.IdentityId,
            request.ApartmentId,
            request.StartDate,
            request.EndDate);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return CreatedAtAction(nameof(GetBooking), new { id = result.Value.Value }, result.Value);
    }
}
