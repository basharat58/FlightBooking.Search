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

        /// <summary>
        /// Searches for Flight Availability.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /FlightAvailability
        ///     {
        ///			"scheduled": "2020-09-18T00:00:00",
        ///			"scheduledTimeDate": "2020-09-18T03:45:00",
        ///			"departure": "2020-09-24T00:00:00",
        ///			"seats": 5,
        ///			"arrivalAirportCode": "RHO",
        ///			"roundTrip": false
        ///	 }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A list of FlightAvailabilityResponse objects</returns>
        /// <response code="200">Returns the list of FlightAvailabilityResponse objects</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(FlightAvailabilityRequest), typeof(FlightAvailabilityRequestExample))]
        public async Task<IActionResult> SearchFlights(FlightAvailabilityRequest request)
        {
            var query = new SearchFlightAvailabilityQuery(
                request.Scheduled, 
                request.ScheduledTimeDate, 
                request.Seats, 
                request.ArrivalAirportCode,
                request.RoundTrip,
                request.Departure);
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No flights were found." });
        }
    }
}