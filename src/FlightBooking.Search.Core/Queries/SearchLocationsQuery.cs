using FlightBooking.Search.Core.Responses;
using MediatR;
using System.Collections.Generic;

namespace FlightBooking.Search.Core.Queries
{
    public class SearchLocationsQuery : IRequest<List<LocationResponse>>
    {
        public SearchLocationsQuery(string locationSearch)
        {
            LocationSearch = locationSearch;
        }

        public string LocationSearch { get; set; }
    }
}
