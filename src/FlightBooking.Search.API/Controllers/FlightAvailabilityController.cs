using FlightBooking.Search.API.Swagger;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.Tasks;
using System.Threading;

namespace FlightBooking.Search.API.Controllers
{
    [Route("api/flightavailability")]
    [ApiController]
    public class FlightAvailabilityController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public FlightAvailabilityController(IMediator mediator)
        {
            _mediatr = mediator;
        }
        
        [HttpPost]
        [SwaggerRequestExample(typeof(FlightAvailabilityRequest), typeof(FlightAvailabilityRequestExample))]
        public async Task<IActionResult> SearchFlights(FlightAvailabilityRequest request)
        {
            var query = new SearchFlightAvailabilityQuery(
                request.Scheduled, 
                request.ScheduledTimeDate, 
                request.Seats, 
                request.ArrivalAirportCode);
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No flights were found." });
        }
    }
}