using Nest;
using System;

namespace FlightBooking.Search.Core.Elasticsearch
{
    public class ElasticSearchClient : IElasticSearchClient
    {
        public IElasticClient CreateElasticClient(string url)
        {
            var settings = new ConnectionSettings(new Uri(url))
                .PrettyJson().DisableDirectStreaming();
            return new ElasticClient(settings);
        }
    }
}
