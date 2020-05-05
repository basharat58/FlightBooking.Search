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
    public class GetAllLocationsHandlerTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public GetAllLocationsHandlerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void WhenHandle()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetAllLocationsHandler>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var locations = AutoFixture.Create<Location[]>().ToList();
            var getAllLocationsQuery = AutoFixture.Create<GetAllLocationsQuery>();
            var locationsResponse = locations.Select(loc => new LocationResponse
            {
                Region = loc.Region,
                Country = loc.Country
            });

            Mocker.GetMock<ILocationRepository>()
                .Setup(lr => lr.GetAllLocations())
                .ReturnsAsync(locations);
            Mocker.GetMock<IMapper>()
                .Setup(map => map.Map(locations))
                .Returns(locationsResponse.ToList());

            // Act
            var result = await subject.Handle(getAllLocationsQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(locationsResponse);
        }

        [Fact]
        public async void WhenHandle_AndNull()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetAllLocationsHandler>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();            
            var getAllLocationsQuery = AutoFixture.Create<GetAllLocationsQuery>();            

            Mocker.GetMock<ILocationRepository>()
                .Setup(lr => lr.GetAllLocations())
                .ReturnsAsync((List<Location>)null);            

            // Act
            var result = await subject.Handle(getAllLocationsQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
