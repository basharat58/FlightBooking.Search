using FlightBooking.Search.Core.Responses;
using MediatR;
using System.Collections.Generic;

namespace FlightBooking.Search.Core.Queries
{
    public class SearchLocationQuery : IRequest<List<LocationResponse>>
    {
        public SearchLocationQuery(string locationSearch)
        {
            LocationSearch = locationSearch;
        }

        public string LocationSearch { get; set; }
    }
}
