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
    public class HotelControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private static Fixture AutoFixture { get; } = new Fixture();

        public HotelControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task WhenAllHotelsAreReturned()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync("api/hotel");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var airlines = JsonConvert.DeserializeObject<List<HotelResponse>>(await response.Content.ReadAsStringAsync());
            airlines.Should().HaveCountGreaterThan(1000);
        }

        [Fact]
        public async Task WhenSearchAirlinesAndResultsReturned()
        {
            // Arrange
            var hotelRequest = new HotelRequest { HotelSearch = "Gavi" };

            // Act
            var response = await _httpClient.PostAsync("api/hotel",
                new StringContent(JsonConvert.SerializeObject(hotelRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

            var hotels = JsonConvert.DeserializeObject<List<HotelResponse>>(await response.Content.ReadAsStringAsync());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            hotels.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public async Task WhenSearchAirlinesAndNoResultsReturned()
        {
            // Arrange
            var hotelRequest = AutoFixture.Create<HotelRequest>();

            // Act
            var response = await _httpClient.PostAsync("api/hotel",
                new StringContent(JsonConvert.SerializeObject(hotelRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

            var hotels = JsonConvert.DeserializeObject<List<HotelResponse>>(await response.Content.ReadAsStringAsync());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            hotels.Count.Should().Be(0);
        }
    }
}
