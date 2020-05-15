using FlightBooking.Search.Core.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace FlightBooking.Search.API.Swagger
{
    public class HotelRequestExample : IExamplesProvider<HotelRequest>
    {
        public HotelRequest GetExamples()
        {
            return new HotelRequest
            {
                HotelSearch = "Grand"
            };
        }
    }
}
