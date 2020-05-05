using FlightBooking.Search.Core.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace FlightBooking.Search.API.Swagger
{
    public class HotelAvailabilityRequestExample : IExamplesProvider<HotelAvailabilityRequest>
    {
        public HotelAvailabilityRequest GetExamples()
        {
            return new HotelAvailabilityRequest
            {
                Infants = 1,
                Children = 2,
                Adults = 2,
                StayDate = new DateTime(2020, 09, 18),
                HotelName = "Lydia Maris",
                NetPrice = 600,
                Region = "Rhodes",
                Country = "Greece"
            };
        }
    }
}
