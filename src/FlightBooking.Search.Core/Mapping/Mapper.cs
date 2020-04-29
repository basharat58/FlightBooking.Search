using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Responses;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Search.Core.Mapping
{
    public class Mapper : IMapper
    {
        public List<HotelAvailabilityResponse> Map(List<HotelAvailability> hotelAvailabilities)
        {
            return hotelAvailabilities.Select(ha => Map(ha)).ToList();
        }

        public HotelAvailabilityResponse Map(HotelAvailability hotelAvailability)
        {
            return new HotelAvailabilityResponse
            {
                Id = hotelAvailability.Id,
                HotelId = hotelAvailability.HotelId,
                Infants = hotelAvailability.Infants,
                Children = hotelAvailability.Children,
                Adults = hotelAvailability.Adults,
                HotelName = hotelAvailability.HotelName,
                Meal = hotelAvailability.Meal,
                Room = hotelAvailability.Room,
                CurrencyCode = hotelAvailability.CurrencyCode,
                NetPrice = hotelAvailability.NetPrice,
                StayDate = hotelAvailability.StayDate,
                EndDate = hotelAvailability.EndDate,
                AirportCode = hotelAvailability.AirportCode,
                Region = hotelAvailability.Region,
                Country = hotelAvailability.Country
            };
        }

        public List<FlightAvailabilityResponse> Map(List<FlightAvailability> flightAvailabilities)
        {
            return flightAvailabilities.Select(fa => Map(fa)).ToList();
        }

        public FlightAvailabilityResponse Map(FlightAvailability flightAvailability)
        {
            return new FlightAvailabilityResponse
            {
                Id = flightAvailability.Id,
                Scheduled = flightAvailability.Scheduled,
                ScheduledTimeDate = flightAvailability.ScheduledTimeDate,
                Arrival = flightAvailability.Arrival,
                ArrivalTimeDate = flightAvailability.ArrivalTimeDate,
                FlightIdentifier = flightAvailability.FlightIdentifier,
                DepartureAirportCode = flightAvailability.DepartureAirportCode,
                DepartureAirport = flightAvailability.DepartureAirport,
                DepartureAirportTimezone = flightAvailability.DepartureAirportTimezone,
                ArrivalAirportCode = flightAvailability.ArrivalAirportCode,
                ArrivalAirport = flightAvailability.ArrivalAirport,
                ArrivalAirportTimezone = flightAvailability.ArrivalAirportTimezone,
                AirportCity = flightAvailability.AirportCity,
                AirportCountry = flightAvailability.AirportCountry,
                Terminal = flightAvailability.Terminal,
                Seats = flightAvailability.Seats,
                Gate = flightAvailability.Gate,
                AirlineIata = flightAvailability.AirlineIata,
                AirlineName = flightAvailability.AirlineName,
                FlightBound = flightAvailability.FlightBound
            };
        }

        public List<LocationResponse> Map(List<Location> locations)
        {
            return locations.Select(loc => Map(loc)).ToList();
        }

        public LocationResponse Map(Location location)
        {
            return new LocationResponse
            {
                Region = location.Region,
                Country = location.Country
            };
        }
    }
}
