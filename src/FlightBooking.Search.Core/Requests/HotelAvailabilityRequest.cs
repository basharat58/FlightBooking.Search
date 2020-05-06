using System;

namespace FlightBooking.Search.Core.Requests
{
    public class HotelAvailabilityRequest
    {
        public int Infants { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }
        public DateTime StayDate { get; set; }
        public string HotelName { get; set; }
        public double NetPrice { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public bool Available { get; set; }
    }
}
