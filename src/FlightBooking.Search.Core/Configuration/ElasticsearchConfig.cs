
namespace FlightBooking.Search.Core.Configuration
{
    public class ElasticsearchConfig
    {
        public string Url { get; set; }
        public string FlightAvailabilityIndex { get; set; }
        public string HotelAvailabilityIndex { get; set; }
        public string LocationIndex { get; set; }
        public string AirlineIndex { get; set; }
        public string HotelIndex { get; set; }
    }
}
