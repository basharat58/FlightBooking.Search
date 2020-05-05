using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Elasticsearch;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.Search.Core.Queries;

namespace FlightBooking.Search.Core.Repositories
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly IOptions<ElasticsearchConfig> _elasticsearchConfig;
        private readonly IElasticSearchClient _elasticSearchClient;

        public AirlineRepository(
            IOptions<ElasticsearchConfig> elasticsearchConfig,
            IElasticSearchClient elasticSearchClient)
        {
            _elasticsearchConfig = elasticsearchConfig;
            _elasticSearchClient = elasticSearchClient;
        }

        public async Task<List<Airline>> GetAllAirlines()
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);
            var response = await client.SearchAsync<Airline>(loc => loc            
                .Index(_elasticsearchConfig.Value.AirlineIndex)
                .Size(100)
                .Query(q => q.MatchAll()));
            return response.Documents?.Select(air => new Airline
            {
                Name = air.Name,
                Code = air.Code,
                Country = air.Country
            }).ToList();
        }

        public async Task<List<Airline>> SearchAirlines(SearchAirlinesQuery airlinesQuery)
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);
            var airlinesResponse = await client.SearchAsync<Airline>(far =>
                far.Index(_elasticsearchConfig.Value.AirlineIndex)
                .Query(q => q
                  .QueryString(qs => qs.Query($"{airlinesQuery.AirlineSearch.ToLower()}*").AnalyzeWildcard(true)))
               );
            return airlinesResponse.Documents?.Select(air => new Airline
            {
                Name = air.Name,
                Code = air.Code,
                Country = air.Country
            }).ToList();
        }
    }
}
