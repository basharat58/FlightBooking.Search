using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBooking.Search.Core.Repositories
{
    public interface IHotelAvailabilityRepository
    {
        Task<List<HotelAvailability>> SearchHotelAvailability(SearchHotelAvailabilityQuery hotelAvailabilityRequest);
    }
}
