using AutoFixture;
using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Repositories;
using FlightBooking.Search.Core.Elasticsearch;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace FlightBooking.Search.Core.Tests.Repositories
{
    public class FlightAvailabilityRepositoryTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public FlightAvailabilityRepositoryTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void When_SearchFlightAvailability()
        {
            // Arrange
            var subject = Mocker.CreateInstance<FlightAvailabilityRepository>();
            var searchFlightAvailabilityQuery = AutoFixture.Create<SearchFlightAvailabilityQuery>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var flightAvailabilityResponse = AutoFixture.Create<FlightAvailability[]>().ToList();
            var mockSearchResponse = new Mock<ISearchResponse<FlightAvailability>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns(flightAvailabilityResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<FlightAvailability>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchFlightAvailability(searchFlightAvailabilityQuery);

            // Assert
            result.Should().BeEquivalentTo(flightAvailabilityResponse);
        }

        [Fact]
        public async void When_SearchFlightAvailability_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<FlightAvailabilityRepository>();
            var searchFlightAvailabilityQuery = AutoFixture.Create<SearchFlightAvailabilityQuery>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();            
            var mockSearchResponse = new Mock<ISearchResponse<FlightAvailability>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<FlightAvailability>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<FlightAvailability>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchFlightAvailability(searchFlightAvailabilityQuery);

            // Assert
            result.Should().BeEquivalentTo(null);
        }
    }
}
