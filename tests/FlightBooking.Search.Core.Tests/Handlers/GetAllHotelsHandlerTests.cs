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
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace FlightBooking.Search.Core.Tests.Handlers
{
    public class GetAllHotelsHandlerTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public GetAllHotelsHandlerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void WhenHandle()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetAllHotelsHandler>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var hotels = AutoFixture.Create<Hotel[]>().ToList();
            var getAllLocationsQuery = AutoFixture.Create<GetAllHotelsQuery>();
            var hotelsResponse = hotels.Select(hot => new HotelResponse
            {
                Id = hot.Id,
                Name = hot.Name,
                Code = hot.Code,
                Country = hot.Country
            });

            Mocker.GetMock<IHotelRepository>()
                .Setup(lr => lr.GetAllHotels())
                .ReturnsAsync(hotels);
            Mocker.GetMock<IMapper>()
                .Setup(map => map.Map(hotels))
                .Returns(hotelsResponse.ToList());

            // Act
            var result = await subject.Handle(getAllLocationsQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(hotelsResponse);
        }

        [Fact]
        public async void WhenHandle_AndNull()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetAllHotelsHandler>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var getAllHotelsQuery = AutoFixture.Create<GetAllHotelsQuery>();

            Mocker.GetMock<IHotelRepository>()
                .Setup(lr => lr.GetAllHotels())
                .ReturnsAsync((List<Hotel>)null);

            // Act
            var result = await subject.Handle(getAllHotelsQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
