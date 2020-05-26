using FlightBooking.Search.Core.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using FlightBooking.Search.Core.Requests;
using System.Text;
using System.Net.Mime;
using System.Linq;

namespace FlightBooking.Search.API.IntegrationTests
{
    public class AirlineControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private static Fixture AutoFixture { get; } = new Fixture();

        public AirlineControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task WhenAllAirlinesAreReturned()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync("api/airline");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var airlines = JsonConvert.DeserializeObject<List<AirlineResponse>>(await response.Content.ReadAsStringAsync());
            airlines.Should().HaveCount(74);
        }

        [Fact]
        public async Task WhenSearchAirlinesAndResultsReturned()
        {
            // Arrange
            var airlineRequest = new AirlineRequest { AirlineSearch = "KLM" };

            // Act
            var response = await _httpClient.PostAsync("api/airline",
                new StringContent(JsonConvert.SerializeObject(airlineRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

            var airline = JsonConvert.DeserializeObject<List<AirlineResponse>>(await response.Content.ReadAsStringAsync()).FirstOrDefault();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            airline.Code.Should().Be("KLM");
            airline.Name.Should().Be("KLM Royal Dutch Airlines");
            airline.Country.Should().Be("Netherlands");
        }

        [Fact]
        public async Task WhenSearchAirlinesAndNoResultsReturned()
        {
            // Arrange
            var airlineRequest = AutoFixture.Create<AirlineRequest>();           

            // Act
            var response = await _httpClient.PostAsync("api/airline", 
                new StringContent(JsonConvert.SerializeObject(airlineRequest), 
                Encoding.UTF8, 
                MediaTypeNames.Application.Json));

            var airlines = JsonConvert.DeserializeObject<List<AirlineResponse>>(await response.Content.ReadAsStringAsync());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            airlines.Should().HaveCount(0);
        }
    }
}
