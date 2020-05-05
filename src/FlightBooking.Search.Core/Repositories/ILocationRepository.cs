using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBooking.Search.Core.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> SearchLocations(SearchLocationsQuery locationQuery);
        Task<List<Location>> GetAllLocations();
    }
}
