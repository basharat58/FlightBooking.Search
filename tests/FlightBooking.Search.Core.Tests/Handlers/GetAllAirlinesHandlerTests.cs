﻿using AutoFixture;
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
    public class GetAllAirlinesHandlerTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public GetAllAirlinesHandlerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void WhenHandle()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetAllAirlinesHandler>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var airlines = AutoFixture.Create<Airline[]>().ToList();
            var getAllLocationsQuery = AutoFixture.Create<GetAllAirlinesQuery>();
            var airlinesResponse = airlines.Select(air => new AirlineResponse
            {
                Name = air.Name,
                Code = air.Code,
                Country = air.Country
            });

            Mocker.GetMock<IAirlineRepository>()
                .Setup(lr => lr.GetAllAirlines())
                .ReturnsAsync(airlines);
            Mocker.GetMock<IMapper>()
                .Setup(map => map.Map(airlines))
                .Returns(airlinesResponse.ToList());

            // Act
            var result = await subject.Handle(getAllLocationsQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(airlinesResponse);
        }

        [Fact]
        public async void WhenHandle_AndNull()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetAllAirlinesHandler>();
            var cancellationToken = AutoFixture.Create<CancellationToken>();
            var getAllLocationsQuery = AutoFixture.Create<GetAllAirlinesQuery>();

            Mocker.GetMock<IAirlineRepository>()
                .Setup(lr => lr.GetAllAirlines())
                .ReturnsAsync((List<Airline>)null);

            // Act
            var result = await subject.Handle(getAllLocationsQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
