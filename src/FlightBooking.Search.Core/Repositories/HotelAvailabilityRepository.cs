using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Elasticsearch;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.Search.Core.Repositories
{
    public class HotelAvailabilityRepository : IHotelAvailabilityRepository
    {
        private readonly IOptions<ElasticsearchConfig> _elasticsearchConfig;
        private readonly IElasticSearchClient _elasticSearchClient;

        public HotelAvailabilityRepository(
            IOptions<ElasticsearchConfig> elasticsearchConfig,
            IElasticSearchClient elasticSearchClient)
        {
            _elasticsearchConfig = elasticsearchConfig;
            _elasticSearchClient = elasticSearchClient;
        }

        public async Task<List<HotelAvailability>> SearchHotelAvailability(SearchHotelAvailabilityQuery hotelAvailabilityRequest)
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);
            var response = await client.SearchAsync<HotelAvailability>(har =>
                har.Index(_elasticsearchConfig.Value.HotelIndex)                
                .Size(60)
                .Query(q => q
                    .Bool(bq => bq                    
                        .Must(
                            fq => fq.Range(r => r.Field(f => f.Infants).LessThanOrEquals(hotelAvailabilityRequest.Infants)),
                            fq => fq.Range(r => r.Field(f => f.Children).LessThanOrEquals(hotelAvailabilityRequest.Children)),
                            fq => fq.Range(r => r.Field(f => f.Adults).LessThanOrEquals(hotelAvailabilityRequest.Adults)),
                            fq => fq.Match(m => m.Field(f => f.StayDate).Query(hotelAvailabilityRequest.StayDate.ToString("yyyy'-'MM'-'dd"))),
                            fq => fq.MatchPhrase(mp => mp.Field(f => f.Region).Query(hotelAvailabilityRequest.Region)),
                            fq => fq.MatchPhrase(mp => mp.Field(f => f.Country).Query(hotelAvailabilityRequest.Country)),
                            fq => fq.MatchPhrase(mp => mp.Field(f => f.HotelName).Query(hotelAvailabilityRequest.HotelName)),
                            fq => fq.Range(r => r.Field(f => f.NetPrice).LessThanOrEquals(hotelAvailabilityRequest.NetPrice))
                        )
                    )
                )
            );
            return response.Documents?.Select(d => new HotelAvailability
            {
                Adults = d.Adults,
                AirportCode = d.AirportCode,
                Children = d.Children,
                Country = d.Country,
                CurrencyCode = d.CurrencyCode,
                EndDate = d.EndDate,
                HotelId = d.HotelId,
                HotelName = d.HotelName,
                Id = d.Id,
                Infants = d.Infants,
                Meal = d.Meal,
                NetPrice = d.NetPrice,
                Region = d.Region,
                Room = d.Room,
                StayDate = d.StayDate
            }).ToList();
        }
    }
}
