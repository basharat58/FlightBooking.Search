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
    [Route("api/hotelavailability")]
    [ApiController]
    public class HotelAvailabilityController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public HotelAvailabilityController(IMediator mediator)
        {
            _mediatr = mediator;
        }
        
        [HttpPost]
        [SwaggerRequestExample(typeof(HotelAvailabilityRequest), typeof(HotelAvailabilityRequestExample))]
        public async Task<IActionResult> SearchHotels(HotelAvailabilityRequest request)
        {
            var query = new SearchHotelAvailabilityQuery(
                request.Infants,
                request.Children,
                request.Adults,
                request.StayDate,
                request.HotelName,
                request.NetPrice,
                request.Region,
                request.Country);
            var result = await _mediatr.Send(query, new CancellationToken());
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(new { Message = $"No Hotels were found." });
        }
    }
}