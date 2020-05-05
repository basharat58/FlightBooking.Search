using FlightBooking.Search.Core.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace FlightBooking.Search.API.Swagger
{
    public class LocationRequestExample : IExamplesProvider<LocationRequest>
    {
        public LocationRequest GetExamples()
        {
            return new LocationRequest
            {
                LocationSearch = "Fuerteventura"
            };
        }
    }
}
