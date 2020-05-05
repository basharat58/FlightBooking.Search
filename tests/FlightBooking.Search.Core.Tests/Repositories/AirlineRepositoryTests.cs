using AutoFixture;
using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Entities;
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
using FlightBooking.Search.Core.Queries;

namespace FlightBooking.Search.Core.Tests.Repositories
{
    public class AirlineRepositoryTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public AirlineRepositoryTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void When_GetAllAirlines()
        {
            // Arrange
            var subject = Mocker.CreateInstance<AirlineRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var airlinesResponse = AutoFixture.Create<Airline[]>().ToList();
            var mockSearchResponse = new Mock<ISearchResponse<Airline>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns(airlinesResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Airline>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.GetAllAirlines();

            // Assert
            result.Should().BeEquivalentTo(airlinesResponse);
        }

        [Fact]
        public async void When_GetAllLocations_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<AirlineRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var mockSearchResponse = new Mock<ISearchResponse<Airline>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<Airline>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Airline>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.GetAllAirlines();

            // Assert
            result.Should().BeEquivalentTo(null);
        }

        [Fact]
        public async void When_SearchAirlines()
        {
            // Arrange
            var subject = Mocker.CreateInstance<AirlineRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var airlinesResponse = AutoFixture.Create<Airline[]>().ToList();
            var airlinesRequest = AutoFixture.Create<SearchAirlinesQuery>();
            var mockSearchResponse = new Mock<ISearchResponse<Airline>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns(airlinesResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Airline>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchAirlines(airlinesRequest);

            // Assert
            result.Should().BeEquivalentTo(airlinesResponse);
        }

        [Fact]
        public async void When_SearchAirlines_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<AirlineRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var airlinesRequest = AutoFixture.Create<SearchAirlinesQuery>();
            var mockSearchResponse = new Mock<ISearchResponse<Airline>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<Airline>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Airline>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchAirlines(airlinesRequest);

            // Assert
            result.Should().BeEquivalentTo(null);
        }
    }
}
