using AutoFixture;
using FlightBooking.Search.Core.Requests;
using FlightBooking.Search.Core.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Search.API.IntegrationTests
{
    public class LocationControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private static Fixture AutoFixture { get; } = new Fixture();

        public LocationControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task WhenAllLocationsAreReturned()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync("api/location");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var locations = JsonConvert.DeserializeObject<List<LocationResponse>>(await response.Content.ReadAsStringAsync());
            locations.Should().HaveCountGreaterThan(10);
        }

        [Fact]
        public async Task WhenSearchLocationsAndResultsReturned()
        {
            // Arrange
            var locationRequest = new LocationRequest { LocationSearch = "Fuerte" };

            // Act
            var response = await _httpClient.PostAsync("api/location",
                new StringContent(JsonConvert.SerializeObject(locationRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

            var locations = JsonConvert.DeserializeObject<List<LocationResponse>>(await response.Content.ReadAsStringAsync());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            locations.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task WhenSearchLocationsAndNoResultsReturned()
        {
            // Arrange
            var locationRequest = AutoFixture.Create<LocationRequest>();

            // Act
            var response = await _httpClient.PostAsync("api/location",
                new StringContent(JsonConvert.SerializeObject(locationRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

            var locations = JsonConvert.DeserializeObject<List<HotelResponse>>(await response.Content.ReadAsStringAsync());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            locations.Count.Should().Be(0);
        }
    }
}
