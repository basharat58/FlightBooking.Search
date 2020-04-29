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
    public class LocationRepositoryTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public LocationRepositoryTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void When_GetAllLocations()
        {
            // Arrange
            var subject = Mocker.CreateInstance<LocationRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var locationsResponse = AutoFixture.Create<Location[]>().ToList();
            var mockSearchResponse = new Mock<ISearchResponse<Location>>();            
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns(locationsResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Location>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.GetAllLocations();

            // Assert
            result.Should().BeEquivalentTo(locationsResponse);
        }

        [Fact]
        public async void When_GetAllLocations_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<LocationRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();            
            var mockSearchResponse = new Mock<ISearchResponse<Location>>();
            var mockElasticClient = new Mock<IElasticClient>();            

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<Location>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Location>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.GetAllLocations();

            // Assert
            result.Should().BeEquivalentTo(null);
        }

        [Fact]
        public async void When_SearchLocations()
        {
            // Arrange
            var subject = Mocker.CreateInstance<LocationRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var locationsResponse = AutoFixture.Create<Location[]>().ToList();
            var locationRequest = AutoFixture.Create<SearchLocationsQuery>();
            var mockSearchResponse = new Mock<ISearchResponse<Location>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns(locationsResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Location>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchLocations(locationRequest);

            // Assert
            result.Should().BeEquivalentTo(locationsResponse);
        }

        [Fact]
        public async void When_SearchLocations_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<LocationRepository>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();
            var locationRequest = AutoFixture.Create<SearchLocationsQuery>();
            var mockSearchResponse = new Mock<ISearchResponse<Location>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<Location>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<Location>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchLocations(locationRequest);

            // Assert
            result.Should().BeEquivalentTo(null);
        }
    }
}
