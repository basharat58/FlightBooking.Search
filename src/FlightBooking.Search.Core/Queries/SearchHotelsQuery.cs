using FlightBooking.Search.Core.Responses;
using System.Collections.Generic;
using MediatR;

namespace FlightBooking.Search.Core.Queries
{
    public class SearchHotelsQuery : IRequest<List<HotelResponse>>
    {
        public SearchHotelsQuery(string airlineSearch)
        {
            AirlineSearch = airlineSearch;
        }

        public string AirlineSearch { get; set; }
    }
}
