using FlightBooking.Search.API.Swagger;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBooking.Search.API.Controllers
{
    [Route("api/airline")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public AirlineController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Returns all the Airlines.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllAirlinesQuery();
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No airlines were found." });
        }

        /// <summary>
        /// Searches for Airlines.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Airline
        ///     {
        ///        "airlineSearch": "KLM"
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A list of AirlineResponse objects</returns>
        /// <response code="200">Returns the list of AirlineResponse objects</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(AirlineRequest), typeof(AirlineRequestExample))]
        public async Task<IActionResult> SearchAirlines(AirlineRequest request)
        {
            var query = new SearchAirlinesQuery(request.AirlineSearch);
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No airlines were found." });
        }
    }
}