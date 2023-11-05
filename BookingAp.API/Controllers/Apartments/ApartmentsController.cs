using BookingAP.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingAp.API.Controllers.Apartments
{
    [ApiController]
    [Route("api/apartments")]
    public class ApartmentsController : ApiController
    {
        private readonly ISender _sender;

        public ApartmentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchApartments(
            DateOnly startDate,
            DateOnly endDate,
            CancellationToken cancellationToken)
        {
            var query = new SearchApartmentsQuery(startDate, endDate);

            var result = await _sender.Send(query, cancellationToken);

            return result.Match(success => Ok(result.Value),
                                Problem);
        }
    }
}