using AutoFixture;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Mapping;
using FlightBooking.Search.Core.Responses;
using FluentAssertions;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FlightBooking.Search.Core.Tests.Mapping
{
    public class MapperTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public MapperTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void When_MapHotelAvailabilityResponses()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var hotelAvailabilities = AutoFixture.Create<HotelAvailability[]>().ToList();
            var hotelsAvailabilityResponse = new List<HotelAvailabilityResponse>();

            foreach (var hotelAvailability in hotelAvailabilities)
            {
                hotelsAvailabilityResponse.Add(new HotelAvailabilityResponse
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
                });
            }

            // Act
            var result = subject.Map(hotelAvailabilities);

            // Assert
            result.Should().BeEquivalentTo(hotelsAvailabilityResponse);
        }

        [Fact]
        public void When_MapHotelAvailabilityResponse()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var hotelAvailability = AutoFixture.Create<HotelAvailability>();
            var hotelsAvailabilityResponse = new HotelAvailabilityResponse {
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

            // Act
            var result = subject.Map(hotelAvailability);

            // Assert
            result.Should().BeEquivalentTo(hotelsAvailabilityResponse);
        }

        [Fact]
        public void When_MapFlightAvailabilityResponses()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var flightAvailabilities = AutoFixture.Create<FlightAvailability[]>().ToList();
            var flightAvailabilityResponse = new List<FlightAvailabilityResponse>();

            foreach (var flightAvailability in flightAvailabilities)
            {
                flightAvailabilityResponse.Add(new FlightAvailabilityResponse
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
                });
            }

            // Act
            var result = subject.Map(flightAvailabilities);

            // Assert
            result.Should().BeEquivalentTo(flightAvailabilityResponse);
        }

        [Fact]
        public void When_MapFlightAvailabilityResponse()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var flightAvailability = AutoFixture.Create<FlightAvailability>();
            var flightAvailabilityResponse = new FlightAvailabilityResponse
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

            // Act
            var result = subject.Map(flightAvailability);

            // Assert
            result.Should().BeEquivalentTo(flightAvailabilityResponse);
        }

        [Fact]
        public void When_MapLocationsResponse()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var locations = AutoFixture.Create<Location[]>().ToList();
            var locationsResponse = new List<LocationResponse>();

            foreach (var location in locations)
            {
                locationsResponse.Add(new LocationResponse
                {
                    Region = location.Region,
                    Country = location.Country
                });
            }

            // Act
            var result = subject.Map(locations);

            // Assert
            result.Should().BeEquivalentTo(locationsResponse);
        }

        [Fact]
        public void When_MapLocationResponse()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var location = AutoFixture.Create<Location>();
            var locationResponse = new LocationResponse
            {
                Region = location.Region,
                Country = location.Country
            };

            // Act
            var result = subject.Map(location);

            // Assert
            result.Should().BeEquivalentTo(locationResponse);
        }

        [Fact]
        public void When_MapAirlinesResponse()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var airlines = AutoFixture.Create<Airline[]>().ToList();
            var airlinesResponse = new List<AirlineResponse>();

            foreach (var airline in airlines)
            {
                airlinesResponse.Add(new AirlineResponse
                {
                    Name = airline.Name,
                    Code = airline.Code,
                    Country = airline.Country
                });
            }

            // Act
            var result = subject.Map(airlines);

            // Assert
            result.Should().BeEquivalentTo(airlinesResponse);
        }

        [Fact]
        public void When_MapAirlineResponse()
        {
            // Arrange
            var subject = Mocker.CreateInstance<Mapper>();
            var airline = AutoFixture.Create<Airline>();
            var airlineResponse = new AirlineResponse
            {
                Name = airline.Name,
                Code = airline.Code,
                Country = airline.Country
            };

            // Act
            var result = subject.Map(airline);

            // Assert
            result.Should().BeEquivalentTo(airlineResponse);
        }
    }
}
