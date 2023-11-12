using BookingAp.Contract.Reviews;
using BookingAP.Application.Reviews.AddReview;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingAp.API.Controllers.Reviews;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ApiController
{
    private readonly ISender _sender;

    public ReviewsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(AddReviewRequest request, CancellationToken cancellationToken)
    {
        var command = new AddReviewCommand(request.BookingId, request.Rating, request.Comment);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Ok(result.Value);
    }
}
