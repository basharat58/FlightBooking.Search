using System.Threading;
using FlightBooking.Search.Core.Queries;
using MediatR;
using System.Threading.Tasks;
using FlightBooking.Search.Core.Responses;
using FlightBooking.Search.Core.Repositories;
using System.Collections.Generic;
using FlightBooking.Search.Core.Mapping;

namespace FlightBooking.Search.Core.Handlers
{
    public class SearchHotelAvailabilityHandler : IRequestHandler<SearchHotelAvailabilityQuery, List<HotelAvailabilityResponse>>
    {
        private readonly IHotelAvailabilityRepository _hotelAvailabilityRepository;
        private readonly IMapper _mapper;

        public SearchHotelAvailabilityHandler(
            IHotelAvailabilityRepository hotelAvailabilityRepository,
            IMapper mapper)
        {
            _hotelAvailabilityRepository = hotelAvailabilityRepository;
            _mapper = mapper;
        }

        public async Task<List<HotelAvailabilityResponse>> Handle(SearchHotelAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var availability = await _hotelAvailabilityRepository.SearchHotelAvailability(request);            
            return availability == null
                ? null
                : _mapper.Map(availability);
        }
    }
}
