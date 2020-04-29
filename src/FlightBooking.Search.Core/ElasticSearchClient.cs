using Nest;
using System;

namespace FlightBooking.Search.Core
{
    public class ElasticSearchClient : IElasticSearchClient
    {
        public IElasticClient CreateElasticClient(string url)
        {
            var settings = new ConnectionSettings(new Uri(url));
            return new ElasticClient(settings);
        }
    }
}
