using FlightBooking.Search.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace FlightBooking.Search.Core.Queries
{
    public class SearchHotelAvailabilityQuery : IRequest<List<HotelAvailabilityResponse>>
    {
        public SearchHotelAvailabilityQuery(int infants,
            int children,
            int adults,
            DateTime stayDate,
            string hotelName,
            double? netPrice,
            string region,
            string country,
            bool available)
        {
            Infants = infants;
            Children = children;
            Adults = adults;
            StayDate = stayDate;
            HotelName = hotelName;
            NetPrice = netPrice;
            Region = region;
            Country = country;
            Available = available;
        }

        public int Infants { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }
        public DateTime StayDate { get; set; }
        public string HotelName { get; set; }
        public double? NetPrice { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public bool Available { get; set; }
    }
}
