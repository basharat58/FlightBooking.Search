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
    public class GetAllLocationsHandler : IRequestHandler<GetAllLocationsQuery, List<LocationResponse>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public GetAllLocationsHandler(
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<List<LocationResponse>> Handle(GetAllLocationsQuery query, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllLocations();
            return locations == null
                ? null
                : _mapper.Map(locations);
        }
    }
}
