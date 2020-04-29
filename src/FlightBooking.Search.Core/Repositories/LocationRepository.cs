using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.Search.Core.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IOptions<ElasticsearchConfig> _elasticsearchConfig;
        private readonly IElasticSearchClient _elasticSearchClient;

        public LocationRepository(
            IOptions<ElasticsearchConfig> elasticsearchConfig,
            IElasticSearchClient elasticSearchClient)
        {
            _elasticsearchConfig = elasticsearchConfig;
            _elasticSearchClient = elasticSearchClient;            
        }

        public async Task<List<Location>> GetAllLocations()
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);
            var response = await client.SearchAsync<Location>(loc => loc
                .Index(_elasticsearchConfig.Value.LocationIndex)
                .Size(150)
                .Query(q => q.MatchAll()));
            return response.Documents?.Select(loc => new Location { 
                Region = loc.Region, 
                Country = loc.Country 
            }).ToList();
        }

        public async Task<List<Location>> SearchLocation(SearchLocationQuery locationQuery)
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);
            var response = await client.SearchAsync<Location>(far =>
                far.Index(_elasticsearchConfig.Value.LocationIndex)
                .From(0)
                .Size(60)
                .Query(q => q
                    .Wildcard(w => w
                        .Name("region")
                        .Value(locationQuery.LocationSearch)
                    )
                )
            );
            return response.Documents?.Select(loc => new Location
            {
                Region = loc.Region,
                Country = loc.Country
            }).ToList();
        }
    }
}
