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
    public class HotelAvailabilityControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private static Fixture AutoFixture { get; } = new Fixture();

        public HotelAvailabilityControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task WhenSearchForHotelsAvailability()
        {
            // Arrange
            var hotelAvailabilityRequest = AutoFixture.Create<HotelAvailabilityRequest>();

            // Act
            var response = await _httpClient.PostAsync("api/flightavailability",
                new StringContent(JsonConvert.SerializeObject(hotelAvailabilityRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
