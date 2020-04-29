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
    public class SearchFlightAvailabilityHandler : IRequestHandler<SearchFlightAvailabilityQuery, List<FlightAvailabilityResponse>>
    {
        private readonly IFlightAvailabilityRepository _flightAvailabilityRepository;
        private readonly IMapper _mapper;

        public SearchFlightAvailabilityHandler(
            IFlightAvailabilityRepository flightAvailabilityRepository,
            IMapper mapper)
        {
            _flightAvailabilityRepository = flightAvailabilityRepository;
            _mapper = mapper;
        }

        public async Task<List<FlightAvailabilityResponse>> Handle(SearchFlightAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var availability = await _flightAvailabilityRepository.SearchFlightAvailability(request);
            return availability == null
                ? null
                : _mapper.Map(availability);                
        }
    }
}
