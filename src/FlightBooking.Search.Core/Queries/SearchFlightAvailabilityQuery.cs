using FlightBooking.Search.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace FlightBooking.Search.Core.Queries
{
    public class SearchFlightAvailabilityQuery : IRequest<List<FlightAvailabilityResponse>>
    {
        public SearchFlightAvailabilityQuery(DateTime scheduled,
            DateTime scheduledTimeDate,
            int seats,
            string arrivalAirportCode,
            bool roundTrip,
            DateTime departure)
        {
            Scheduled = scheduled;
            ScheduledTimeDate = scheduledTimeDate;
            Seats = seats;
            ArrivalAirportCode = arrivalAirportCode;
            RoundTrip = roundTrip;
            Departure = departure;
        }

        public DateTime Scheduled { get; set; }
        public DateTime ScheduledTimeDate { get; set; }
        public DateTime Departure { get; set; }
        public int Seats { get; set; }
        public string ArrivalAirportCode { get; set; }
        public bool RoundTrip { get; set; }
    }
}
