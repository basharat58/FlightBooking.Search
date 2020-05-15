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
    public class SearchHotelsHandlerTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public SearchHotelsHandlerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void WhenHandle()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchHotelsHandler>();
            var searchHotelQuery = AutoFixture.Create<SearchHotelsQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var hotels = AutoFixture.Create<Hotel[]>().ToList();
            var hotelsResponse = hotels.Select(hot => new HotelResponse
            {
                Id = hot.Id,
                Name = hot.Name,
                Code = hot.Code,
                Country = hot.Country
            });

            Mocker.GetMock<IHotelRepository>()
                .Setup(lr => lr.SearchHotels(searchHotelQuery))
                .ReturnsAsync(hotels);
            Mocker.GetMock<IMapper>()
                .Setup(map => map.Map(hotels))
                .Returns(hotelsResponse.ToList());

            // Act
            var result = await subject.Handle(searchHotelQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(hotelsResponse);
        }

        [Fact]
        public async void WhenHandle_AndNull()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchHotelsHandler>();
            var searchHotelQuery = AutoFixture.Create<SearchHotelsQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();

            Mocker.GetMock<IHotelRepository>()
                .Setup(lr => lr.SearchHotels(searchHotelQuery))
                .ReturnsAsync((List<Hotel>)null);

            // Act
            var result = await subject.Handle(searchHotelQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
