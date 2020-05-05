using FlightBooking.Search.Core.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace FlightBooking.Search.API.Swagger
{
    public class FlightAvailabilityRequestExample : IExamplesProvider<FlightAvailabilityRequest>
    {
        public FlightAvailabilityRequest GetExamples()
        {
            return new FlightAvailabilityRequest
            {
                Scheduled = new DateTime(2020, 09, 18),
                ScheduledTimeDate = new DateTime(2020, 09, 18).AddHours(3).AddMinutes(45),
                Seats = 5,
                ArrivalAirportCode = "RHO"
            };
        }
    }
}
