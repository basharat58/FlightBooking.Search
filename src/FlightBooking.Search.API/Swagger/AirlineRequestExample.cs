using FlightBooking.Search.Core.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace FlightBooking.Search.API.Swagger
{
    public class AirlineRequestExample : IExamplesProvider<AirlineRequest>
    {
        public AirlineRequest GetExamples()
        {
            return new AirlineRequest
            {
                AirlineSearch = "KLM"
            };
        }
    }
}
