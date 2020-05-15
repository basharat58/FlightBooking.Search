using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Entities;
using FlightBooking.Search.Core.Queries;
using FlightBooking.Search.Core.Elasticsearch;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Nest;

namespace FlightBooking.Search.Core.Repositories
{
    public class FlightAvailabilityRepository : IFlightAvailabilityRepository
    {
        private readonly IOptions<ElasticsearchConfig> _elasticsearchConfig;
        private readonly IElasticSearchClient _elasticSearchClient;

        public FlightAvailabilityRepository(
            IOptions<ElasticsearchConfig> elasticsearchConfig,
            IElasticSearchClient elasticSearchClient)
        {
            _elasticsearchConfig = elasticsearchConfig;
            _elasticSearchClient = elasticSearchClient;
        }

        public async Task<List<FlightAvailability>> SearchFlightAvailability(SearchFlightAvailabilityQuery flightAvailabilityRequest)
        {
            var client = _elasticSearchClient.CreateElasticClient(_elasticsearchConfig.Value.Url);            
            ISearchResponse <FlightAvailability> inboundResponse = null;
            var response = await client.SearchAsync<FlightAvailability>(far =>
                far.Index(_elasticsearchConfig.Value.FlightAvailabilityIndex)
                .From(0)
                .Size(6)
                .Sort(s => s
                    .Ascending(f => f.Id))
                .Query(q => q
                    .Bool(bq => bq
                        .Filter(
                            fq => fq.Range(r => r.Field(f => f.Seats).GreaterThanOrEquals(flightAvailabilityRequest.Seats)),
                            fq => fq.Match(m => m.Field(f => f.Scheduled).Query(flightAvailabilityRequest.Scheduled.ToString("yyyy'-'MM'-'dd"))),
                            fq => fq.MatchPhrase(mp => mp.Field(f => f.ArrivalAirportCode).Query(flightAvailabilityRequest.ArrivalAirportCode))                            
                        )
                    )
                )
            );

            if (flightAvailabilityRequest.RoundTrip && response.Documents.Any())
            {
                var airlines = String.Join(",", response.Documents.Select(d => d.AirlineIata.ToLower()));
                inboundResponse = await client.SearchAsync<FlightAvailability>(far =>
                    far.Index(_elasticsearchConfig.Value.FlightAvailabilityIndex)
                    .From(0)
                    .Size(6)
                    .Sort(s => s
                        .Ascending(f => f.Id))
                    .Query(q => q
                        .Bool(bq => bq
                            .Filter(
                                fq => fq.Range(r => r.Field(f => f.Seats).GreaterThanOrEquals(flightAvailabilityRequest.Seats)),
                                fq => fq.Match(m => m.Field(f => f.Scheduled).Query(flightAvailabilityRequest.Departure.ToString("yyyy'-'MM'-'dd"))),
                                fq => fq.MatchPhrase(mp => mp.Field(f => f.DepartureAirportCode).Query(flightAvailabilityRequest.ArrivalAirportCode))                                
                            ).Should(s => s.Terms(t => t.Field(f => f.AirlineIata).Terms(airlines)))
                        )
                    )
                );                
            }

            var availability = new List<FlightAvailability>();
            if (response?.Documents != null)
            {
                availability = response.Documents?.Select(d => new FlightAvailability
                {
                    Id = d.Id,
                    Scheduled = d.Scheduled,
                    ScheduledTimeDate = d.ScheduledTimeDate,
                    Arrival = d.Arrival,
                    ArrivalTimeDate = d.ArrivalTimeDate,
                    FlightIdentifier = d.FlightIdentifier,
                    DepartureAirportCode = d.DepartureAirportCode,
                    DepartureAirport = d.DepartureAirport,
                    DepartureAirportTimezone = d.DepartureAirportTimezone,
                    ArrivalAirportCode = d.ArrivalAirportCode,
                    ArrivalAirport = d.ArrivalAirport,
                    ArrivalAirportTimezone = d.ArrivalAirportTimezone,
                    AirportCity = d.AirportCity,
                    AirportCountry = d.AirportCountry,
                    Terminal = d.Terminal,
                    Seats = d.Seats,
                    Gate = d.Gate,
                    AirlineIata = d.AirlineIata,
                    AirlineName = d.AirlineName,
                    FlightBound = d.FlightBound
                }).ToList();

                if (inboundResponse?.Documents != null)
                {
                    availability.AddRange(inboundResponse.Documents?.Select(d => new FlightAvailability
                    {
                        Id = d.Id,
                        Scheduled = d.Scheduled,
                        ScheduledTimeDate = d.ScheduledTimeDate,
                        Arrival = d.Arrival,
                        ArrivalTimeDate = d.ArrivalTimeDate,
                        FlightIdentifier = d.FlightIdentifier,
                        DepartureAirportCode = d.DepartureAirportCode,
                        DepartureAirport = d.DepartureAirport,
                        DepartureAirportTimezone = d.DepartureAirportTimezone,
                        ArrivalAirportCode = d.ArrivalAirportCode,
                        ArrivalAirport = d.ArrivalAirport,
                        ArrivalAirportTimezone = d.ArrivalAirportTimezone,
                        AirportCity = d.AirportCity,
                        AirportCountry = d.AirportCountry,
                        Terminal = d.Terminal,
                        Seats = d.Seats,
                        Gate = d.Gate,
                        AirlineIata = d.AirlineIata,
                        AirlineName = d.AirlineName,
                        FlightBound = d.FlightBound
                    }).ToList());
                }
            }
            return availability;            
        }
    }
}
