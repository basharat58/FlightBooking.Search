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
    public class HotelAvailabilityRepositoryTests
    {
        private Fixture AutoFixture { get; }
        private AutoMocker Mocker { get; }

        public HotelAvailabilityRepositoryTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public async void When_SearchHotelAvailability()
        {
            // Arrange
            var subject = Mocker.CreateInstance<HotelAvailabilityRepository>();
            var searchHotelAvailabilityQuery = AutoFixture.Create<SearchHotelAvailabilityQuery>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();            
            var hotelAvailabilityResponse = AutoFixture.Create<HotelAvailability[]>().ToList();
            var mockSearchResponse = new Mock<ISearchResponse<HotelAvailability>>();
            var mockElasticClient = new Mock<IElasticClient>();
            hotelAvailabilityResponse.Select(har => har.Available = true);

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);            
            mockSearchResponse.Setup(x => x.Documents).Returns(hotelAvailabilityResponse);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<HotelAvailability>, ISearchRequest>>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchHotelAvailability(searchHotelAvailabilityQuery);

            // Assert
            result.Should().BeEquivalentTo(hotelAvailabilityResponse);
        }

        [Fact]
        public async void When_SearchHotelAvailability_AndNoResults()
        {
            // Arrange
            var subject = Mocker.CreateInstance<HotelAvailabilityRepository>();
            var searchHotelAvailabilityQuery = AutoFixture.Create<SearchHotelAvailabilityQuery>();
            var elasticsearchConfig = AutoFixture.Create<ElasticsearchConfig>();            
            var mockSearchResponse = new Mock<ISearchResponse<HotelAvailability>>();
            var mockElasticClient = new Mock<IElasticClient>();

            Mocker.GetMock<IOptions<ElasticsearchConfig>>()
                .Setup(esc => esc.Value)
                .Returns(elasticsearchConfig);
            Mocker.GetMock<IElasticSearchClient>()
                .Setup(esc => esc.CreateElasticClient(elasticsearchConfig.Url))
                .Returns(mockElasticClient.Object);
            mockSearchResponse.Setup(x => x.Documents).Returns((List<HotelAvailability>)null);
            mockElasticClient
                .Setup(ec => ec.SearchAsync(
                    It.IsAny<Func<SearchDescriptor<HotelAvailability>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSearchResponse.Object);

            // Act
            var result = await subject.SearchHotelAvailability(searchHotelAvailabilityQuery);

            // Assert
            result.Should().BeEquivalentTo(null);
        }
    }
}
