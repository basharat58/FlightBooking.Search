using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBooking.Search.Core.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> SearchLocation(SearchLocationQuery locationQuery);
        Task<List<Location>> GetAllLocations();
    }
}
