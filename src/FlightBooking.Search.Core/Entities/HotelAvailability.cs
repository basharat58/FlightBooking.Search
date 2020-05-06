using Nest;
using System;

namespace FlightBooking.Search.Core.Entities
{
    public class HotelAvailability
    {
        public int Id { get; set; }
        
        [Text(Name = "hotelid")]
        public int HotelId { get; set; }

        [Text(Name = "hotelname")]
        public string HotelName { get; set; }
        public int Infants { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }

        [Text(Name = "netprice")]
        public double NetPrice { get; set; }
        public string Meal { get; set; }
        public string Room { get; set; }

        [Text(Name = "currencycode")]
        public string CurrencyCode { get; set; }

        [Text(Name = "airportcode")]
        public string AirportCode { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }

        [Text(Name = "staydate")]
        public DateTime StayDate { get; set; }

        [Text(Name = "enddate")]
        public DateTime EndDate { get; set; }

        [Text(Name = "available")]
        public bool Available { get; set; }
    }
}
