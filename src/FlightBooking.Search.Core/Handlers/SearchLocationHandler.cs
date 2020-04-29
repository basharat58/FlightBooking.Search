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
    public class SearchLocationHandler : IRequestHandler<SearchLocationQuery, List<LocationResponse>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public SearchLocationHandler(
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<List<LocationResponse>> Handle(SearchLocationQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.SearchLocation(request);
            return locations == null
                ? null
                : _mapper.Map(locations);
        }
    }
}
