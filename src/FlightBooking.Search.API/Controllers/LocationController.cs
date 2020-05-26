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
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public LocationController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Returns all the Locations.
        /// </summary>
        [HttpGet]        
        public async Task<IActionResult> Get()
        {
            var query = new GetAllLocationsQuery();
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No locations were found." });
        }

        /// <summary>
        /// Searches for Locations.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Location
        ///     {
        ///        "locationSearch": "Fuerteventura"
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A list of LocationResponse objects</returns>
        /// <response code="200">Returns the list of LocationResponse objects</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(LocationRequest), typeof(LocationRequestExample))]
        public async Task<IActionResult> SearchLocations(LocationRequest request)
        {
            var query = new SearchLocationsQuery(request.LocationSearch);
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No locations were found." });
        }
    }
}