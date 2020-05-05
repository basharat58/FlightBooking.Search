
using System;

namespace FlightBooking.Search.Core.Requests
{
    public class FlightAvailabilityRequest
    {
        public DateTime Scheduled { get; set; }
        public DateTime ScheduledTimeDate { get; set; }
        public int Seats { get; set; }
        public string ArrivalAirportCode { get; set; }
    }
}
