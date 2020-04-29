using AutoFixture;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Handlers;
using FlightBooking.Search.Core.Mapping;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Repositories;
using FlightBooking.Search.Core.Responses;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace FlightBooking.Search.Core.Tests.Handlers
{
    public class SearchFlightAvailabilityHandlerTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public SearchFlightAvailabilityHandlerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void WhenHandle()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchFlightAvailabilityHandler>();
            var searchFlightAvailabilityQuery = AutoFixture.Create<SearchFlightAvailabilityQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var flightAvailability = AutoFixture.Create<FlightAvailability[]>().ToList();
            var flightAvailabilityResponse = flightAvailability.Select(fa => new FlightAvailabilityResponse
            {
                Id = fa.Id,
                Scheduled = fa.Scheduled,
                ScheduledTimeDate = fa.ScheduledTimeDate,
                Arrival = fa.Arrival,
                ArrivalTimeDate = fa.ArrivalTimeDate,
                FlightIdentifier = fa.FlightIdentifier,
                DepartureAirportCode = fa.DepartureAirportCode,
                DepartureAirport = fa.DepartureAirport,
                DepartureAirportTimezone = fa.DepartureAirportTimezone,
                ArrivalAirportCode = fa.ArrivalAirportCode,
                ArrivalAirport = fa.ArrivalAirport,
                ArrivalAirportTimezone = fa.ArrivalAirportTimezone,
                AirportCity = fa.AirportCity,
                AirportCountry = fa.AirportCountry,
                Terminal = fa.Terminal,
                Seats = fa.Seats,
                Gate = fa.Gate,
                AirlineIata = fa.AirlineIata,
                AirlineName = fa.AirlineName,
                FlightBound = fa.FlightBound
            });

            Mocker.GetMock<IFlightAvailabilityRepository>()
                .Setup(har => har.SearchFlightAvailability(searchFlightAvailabilityQuery))
                .ReturnsAsync(flightAvailability);
            Mocker.GetMock<IMapper>()
                .Setup(map => map.Map(flightAvailability))
                .Returns(flightAvailabilityResponse.ToList());

            // Act
            var result = await subject.Handle(searchFlightAvailabilityQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(flightAvailabilityResponse);
        }

        [Fact]
        public async void WhenHandle_AndNull()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchFlightAvailabilityHandler>();
            var searchFlightAvailabilityQuery = AutoFixture.Create<SearchFlightAvailabilityQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();

            Mocker.GetMock<IFlightAvailabilityRepository>()
                .Setup(har => har.SearchFlightAvailability(searchFlightAvailabilityQuery))
                .ReturnsAsync((List<FlightAvailability>)null);

            // Act
            var result = await subject.Handle(searchFlightAvailabilityQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
