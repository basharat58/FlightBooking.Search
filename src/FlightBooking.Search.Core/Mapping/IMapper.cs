using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Responses;
using System.Collections.Generic;

namespace FlightBooking.Search.Core.Mapping
{
    public interface IMapper
    {
        List<HotelAvailabilityResponse> Map(List<HotelAvailability> hotelAvailabilities);
        HotelAvailabilityResponse Map(HotelAvailability hotelAvailability);
        List<FlightAvailabilityResponse> Map(List<FlightAvailability> flightAvailabilities);
        FlightAvailabilityResponse Map(FlightAvailability hotelAvailability);
        List<LocationResponse> Map(List<Location> locations);
        LocationResponse Map(Location location);
        List<AirlineResponse> Map(List<Airline> airlines);
        AirlineResponse Map(Airline airline);
        List<HotelResponse> Map(List<Hotel> hotels);
        HotelResponse Map(Hotel hotel);
    }
}
