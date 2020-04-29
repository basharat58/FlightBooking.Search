using Nest;

namespace FlightBooking.Search.Core
{
    public interface IElasticSearchClient
    {
        IElasticClient CreateElasticClient(string url);
    }
}
