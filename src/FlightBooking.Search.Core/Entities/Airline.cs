using Nest;

namespace FlightBooking.Search.Core.Entities
{
    public class Airline
    {
        [Text(Name = "name")]
        public string Name { get; set; }

        [Text(Name = "code")]
        public string Code { get; set; }

        [Text(Name = "country")]
        public string Country { get; set; }
    }
}
