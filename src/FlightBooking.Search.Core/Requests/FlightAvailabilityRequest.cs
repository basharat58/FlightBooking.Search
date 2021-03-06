﻿
using System;

namespace FlightBooking.Search.Core.Requests
{
    public class FlightAvailabilityRequest
    {
        public DateTime Scheduled { get; set; }
        public DateTime ScheduledTimeDate { get; set; }
        public DateTime Departure { get; set; }
        public int Seats { get; set; }
        public string ArrivalAirportCode { get; set; }
        public bool RoundTrip { get; set; }
    }
}
