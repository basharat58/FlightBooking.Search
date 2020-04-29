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
    public class SearchLocationHandlerTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public SearchLocationHandlerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void WhenHandle()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchLocationHandler>();
            var searchLocationQuery = AutoFixture.Create<SearchLocationQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var locations = AutoFixture.Create<Location[]>().ToList();
            var locationsResponse = locations.Select(loc => new LocationResponse
            {
                Region = loc.Region,
                Country = loc.Country
            });

            Mocker.GetMock<ILocationRepository>()
                .Setup(lr => lr.SearchLocation(searchLocationQuery))
                .ReturnsAsync(locations);
            Mocker.GetMock<IMapper>()
                .Setup(map => map.Map(locations))
                .Returns(locationsResponse.ToList());

            // Act
            var result = await subject.Handle(searchLocationQuery, cancellationToken);            

            // Assert
            result.Should().BeEquivalentTo(locationsResponse);
        }

        [Fact]
        public async void WhenHandle_AndNull()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchLocationHandler>();
            var searchLocationQuery = AutoFixture.Create<SearchLocationQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();            

            Mocker.GetMock<ILocationRepository>()
                .Setup(lr => lr.SearchLocation(searchLocationQuery))
                .ReturnsAsync((List<Location>)null);

            // Act
            var result = await subject.Handle(searchLocationQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
