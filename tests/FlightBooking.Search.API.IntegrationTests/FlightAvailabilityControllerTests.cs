using AutoFixture;
using FlightBooking.Search.Core.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlightBooking.Search.API.IntegrationTests
{
    public class FlightAvailabilityControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private static Fixture AutoFixture { get; } = new Fixture();

        public FlightAvailabilityControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task WhenSearchForFlightsAvailability()
        {
            // Arrange
            var flightAvailabilityRequest = AutoFixture.Create<FlightAvailabilityRequest>();

            // Act
            var response = await _httpClient.PostAsync("api/flightavailability",
                new StringContent(JsonConvert.SerializeObject(flightAvailabilityRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
