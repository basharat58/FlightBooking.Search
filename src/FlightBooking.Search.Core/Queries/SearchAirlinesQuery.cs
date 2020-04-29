using FlightBooking.Search.Core.Responses;
using System.Collections.Generic;
using MediatR;

namespace FlightBooking.Search.Core.Queries
{
    public class SearchAirlinesQuery : IRequest<List<AirlineResponse>>
    {
        public SearchAirlinesQuery(string airlineSearch)
        {
            AirlineSearch = airlineSearch;
        }

        public string AirlineSearch { get; set; }
    }
}
