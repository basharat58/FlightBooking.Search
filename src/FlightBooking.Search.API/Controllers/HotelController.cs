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
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public HotelController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Returns all the Hotels.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllHotelsQuery();
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No hotels were found." });
        }

        /// <summary>
        /// Searches for Hotels.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Hotel
        ///     {
        ///        "hotelSearch": "Grand"
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A list of HotelResponse objects</returns>
        /// <response code="200">Returns the list of HotelResponse objects</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(HotelRequest), typeof(HotelRequestExample))]
        public async Task<IActionResult> SearchHotels(HotelRequest request)
        {
            var query = new SearchHotelsQuery(request.HotelSearch);
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No hotels were found." });
        }
    }
}