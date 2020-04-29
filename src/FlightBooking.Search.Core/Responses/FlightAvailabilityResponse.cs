using System;

namespace FlightBooking.Search.Core.Responses
{
    public class FlightAvailabilityResponse
    {
        public int Id { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime ScheduledTimeDate { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime ArrivalTimeDate { get; set; }
        public int Terminal { get; set; }
        public int Seats { get; set; }
        public string Gate { get; set; }
        public string DepartureAirportCode { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureAirportTimezone { get; set; }
        public string ArrivalAirportCode { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalAirportTimezone { get; set; }
        public string AirportCity { get; set; }
        public string AirportCountry { get; set; }        
        public string AirlineIata { get; set; }
        public string AirlineName { get; set; }
        public string FlightIdentifier { get; set; }
        public string FlightBound { get; set; }
    }
}
