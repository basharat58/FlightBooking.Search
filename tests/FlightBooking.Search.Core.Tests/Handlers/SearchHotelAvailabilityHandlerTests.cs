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
    public class SearchHotelAvailabilityHandlerTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public SearchHotelAvailabilityHandlerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void WhenHandle()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchHotelAvailabilityHandler>();
            var searchHotelAvailabilityQuery = AutoFixture.Create<SearchHotelAvailabilityQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var hotelAvailability = AutoFixture.Create<HotelAvailability[]>().ToList();
            var hotelAvailabilityResponse = hotelAvailability.Select(ha => new HotelAvailabilityResponse
            {
                Id = ha.Id,
                HotelId = ha.HotelId,
                Infants = ha.Infants,
                Children = ha.Children,
                Adults = ha.Adults,
                HotelName = ha.HotelName,
                Meal = ha.Meal,
                Room = ha.Room,
                CurrencyCode = ha.CurrencyCode,
                NetPrice = ha.NetPrice,
                StayDate = ha.StayDate,
                EndDate = ha.EndDate,
                AirportCode = ha.AirportCode,
                Region = ha.Region,
                Country = ha.Country
            });

            Mocker.GetMock<IHotelAvailabilityRepository>()
                .Setup(har => har.SearchHotelAvailability(searchHotelAvailabilityQuery))
                .ReturnsAsync(hotelAvailability);
            Mocker.GetMock<IMapper>()
                .Setup(map => map.Map(hotelAvailability))
                .Returns(hotelAvailabilityResponse.ToList());

            // Act
            var result = await subject.Handle(searchHotelAvailabilityQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(hotelAvailabilityResponse);
        }

        [Fact]
        public async void WhenHandle_AndNull()
        {
            // Arrange
            var subject = Mocker.CreateInstance<SearchHotelAvailabilityHandler>();
            var searchHotelAvailabilityQuery = AutoFixture.Create<SearchHotelAvailabilityQuery>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();            

            Mocker.GetMock<IHotelAvailabilityRepository>()
                .Setup(har => har.SearchHotelAvailability(searchHotelAvailabilityQuery))
                .ReturnsAsync((List<HotelAvailability>)null);            

            // Act
            var result = await subject.Handle(searchHotelAvailabilityQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
