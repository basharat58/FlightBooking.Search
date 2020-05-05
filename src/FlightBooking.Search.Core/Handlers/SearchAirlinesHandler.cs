using FlightBooking.Search.Core.Mapping;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Repositories;
using FlightBooking.Search.Core.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;

namespace FlightBooking.Search.Core.Handlers
{
    public class SearchAirlinesHandler : IRequestHandler<SearchAirlinesQuery, List<AirlineResponse>>
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper _mapper;

        public SearchAirlinesHandler(
            IAirlineRepository airlineRepository,
            IMapper mapper)
        {
            _airlineRepository = airlineRepository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task<List<AirlineResponse>> Handle(SearchAirlinesQuery request, CancellationToken cancellationToken)
        {
            var airlines = await _airlineRepository.SearchAirlines(request);
            return airlines == null
                ? null
                : _mapper.Map(airlines);
        }
    }
}
