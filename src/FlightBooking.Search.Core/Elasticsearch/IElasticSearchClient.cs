using Nest;

namespace FlightBooking.Search.Core.Elasticsearch
{
    public interface IElasticSearchClient
    {
        IElasticClient CreateElasticClient(string url);
    }
}
