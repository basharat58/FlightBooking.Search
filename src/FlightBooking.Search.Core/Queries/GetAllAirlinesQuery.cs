using FlightBooking.Search.Core.Responses;
using MediatR;
using System.Collections.Generic;

namespace FlightBooking.Search.Core.Queries
{
    public class GetAllAirlinesQuery : IRequest<List<AirlineResponse>>
    {

    }
}
