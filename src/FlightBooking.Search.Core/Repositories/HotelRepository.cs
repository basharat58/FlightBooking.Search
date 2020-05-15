using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Elasticsearch;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.Search.Core.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IOptions<ElasticsearchConfig> _elasticsearchConfig;
        private readonly IElasticSearchClient _elasticSearchClient;

        public HotelRepository(
            IOptions<ElasticsearchConfig> elasticsearchConfig,
            IElasticSearchClient elasticSearchClient)
        {
            _elasticsearchConfig = elasticsearchConfig;
            _elasticSearchClient = elasticSearchClient;
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);
            var response = await client.SearchAsync<Hotel>(loc => loc
                .Index(_elasticsearchConfig.Value.HotelIndex)
                .Size(2000)
                .Query(q => q.MatchAll()));
            return response.Documents?.Select(hot => new Hotel
            {
                Id = hot.Id,
                Name = hot.Name,
                Code = hot.Code,
                Country = hot.Country
            }).ToList();
        }

        public async Task<List<Hotel>> SearchHotels(SearchHotelsQuery hotelsQuery)
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);
            var hotelsResponse = await client.SearchAsync<Hotel>(hot => hot
                .Index(_elasticsearchConfig.Value.HotelIndex)
                .Size(100)
                .Query(q => q
                  .QueryString(qs => qs.Query($"{hotelsQuery.AirlineSearch.ToLower()}*").AnalyzeWildcard(true)))
               );
            return hotelsResponse.Documents?.Select(hot => new Hotel
            {
                Id = hot.Id,
                Name = hot.Name,
                Code = hot.Code,
                Country = hot.Country
            }).ToList();
        }
    }
}
