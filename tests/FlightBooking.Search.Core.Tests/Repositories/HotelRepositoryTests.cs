using AutoFixture;
using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Elasticsearch;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Repositories;
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
    public class HotelRepositoryTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public HotelRepositoryTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void When_GetAllAirlines()
        {
            // Arrange
            var subject = Mocker.CreateInstance<HotelRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var hotelsResponse = AutoFixture.Create<Hotel[]>().ToList();
            var mockSearchResponse = new Mock<ISearchResponse<Hotel>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns(hotelsResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Hotel>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.GetAllHotels();

            // Assert
            result.Should().BeEquivalentTo(hotelsResponse);
        }

        [Fact]
        public async void When_GetAllLocations_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<HotelRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var mockSearchResponse = new Mock<ISearchResponse<Hotel>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<Hotel>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Hotel>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.GetAllHotels();

            // Assert
            result.Should().BeEquivalentTo(null);
        }

        [Fact]
        public async void When_SearchAirlines()
        {
            // Arrange
            var subject = Mocker.CreateInstance<HotelRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var hotelsResponse = AutoFixture.Create<Hotel[]>().ToList();
            var hotelsRequest = AutoFixture.Create<SearchHotelsQuery>();
            var mockSearchResponse = new Mock<ISearchResponse<Hotel>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns(hotelsResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Hotel>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchHotels(hotelsRequest);

            // Assert
            result.Should().BeEquivalentTo(hotelsResponse);
        }

        [Fact]
        public async void When_SearchAirlines_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<HotelRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var hotelsRequest = AutoFixture.Create<SearchHotelsQuery>();
            var mockSearchResponse = new Mock<ISearchResponse<Hotel>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<Hotel>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Hotel>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchHotels(hotelsRequest);

            // Assert
            result.Should().BeEquivalentTo(null);
        }
    }
}
