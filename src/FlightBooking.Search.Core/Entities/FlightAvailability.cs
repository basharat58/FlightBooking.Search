using Nest;
using System;

namespace FlightBooking.Search.Core.Entities
{
    public class FlightAvailability
    {
        public int Id { get; set; }
        public DateTime Scheduled { get; set; }

        [Text(Name = "scheduledtimedate")]
        public DateTime ScheduledTimeDate { get; set; }
        public DateTime Arrival { get; set; }

        [Text(Name = "arrivaltimedate")]
        public DateTime ArrivalTimeDate { get; set; }

        [Text(Name = "flightidentifier")]
        public string FlightIdentifier { get; set; }

        [Text(Name = "departureairportcode")]
        public string DepartureAirportCode { get; set; }

        [Text(Name = "departureairport")]
        public string DepartureAirport { get; set; }

        [Text(Name = "departureairporttimezone")]
        public string DepartureAirportTimezone { get; set; }

        [Text(Name = "arrivalairportcode")]
        public string ArrivalAirportCode { get; set; }

        [Text(Name = "arrivalairport")]
        public string ArrivalAirport { get; set; }

        [Text(Name = "arrivalairporttimezone")]
        public string ArrivalAirportTimezone { get; set; }

        [Text(Name = "airportcity")]
        public string AirportCity { get; set; }

        [Text(Name = "airportcountry")]
        public string AirportCountry { get; set; }
        public int Terminal { get; set; }
        public int Seats { get; set; }
        public string Gate { get; set; }

        [Text(Name = "airlineiata")]
        public string AirlineIata { get; set; }

        [Text(Name = "airlinename")]
        public string AirlineName { get; set; }

        [Text(Name = "flightbound")]
        public string FlightBound { get; set; }
    }
}
