using FlightBooking.Search.Core.Mapping;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Repositories;
using FlightBooking.Search.Core.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBooking.Search.Core.Handlers
{
    public class GetAllAirlinesHandler : IRequestHandler<GetAllAirlinesQuery, List<AirlineResponse>>
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper _mapper;

        public GetAllAirlinesHandler(
            IAirlineRepository airlineRepository,
            IMapper mapper)
        {
            _airlineRepository = airlineRepository;
            _mapper = mapper;
        }

        public async Task<List<AirlineResponse>> Handle(GetAllAirlinesQuery request, CancellationToken cancellationToken)
        {
            var airlines = await _airlineRepository.GetAllAirlines();
            return airlines == null
                ? null
                : _mapper.Map(airlines);                 
        }
    }
}
