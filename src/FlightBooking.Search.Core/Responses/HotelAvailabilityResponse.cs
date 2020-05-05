using System;

namespace FlightBooking.Search.Core.Responses
{
    public class HotelAvailabilityResponse
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int Infants { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }
        public string HotelName { get; set; }
        public string Meal { get; set; }
        public string Room { get; set; }
        public string CurrencyCode { get; set; }
        public double NetPrice { get; set; }
        public DateTime StayDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AirportCode { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
