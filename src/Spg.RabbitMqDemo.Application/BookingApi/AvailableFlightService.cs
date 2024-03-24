using Spg.RabbitMqDemo.DomainModel.Interfaces;
using Spg.RabbitMqDemo.DomainModel.Model.BookingApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Spg.RabbitMqDemo.Application.BookingApi
{
    public class AvailableFlightService : IAvailableFlightService
    {
        private readonly IAvailableFlightRepository _availableFlightRepository;
        private readonly IIdentifierService _identifierService;

        public AvailableFlightService(
            IAvailableFlightRepository availableFlightRepository, 
            IIdentifierService identifierService)
        {
            _availableFlightRepository = availableFlightRepository;
            _identifierService = identifierService;
        }

        public IQueryable<AvailableFlight> GetAll()
        {
            return _availableFlightRepository.GetAll();
        }

        public void PersistReceivedFlight(IFlightCreation flightData)
        {
            // Validations...
            if (flightData.FromDestination.Continent.ToLower() != "europa"
                || flightData.ToDestination.Continent.ToLower() != "europa")
            {
                // TODO: Log...
                return;
            }

            // Calculations...
            decimal price = (new Random().Next(100, 1500)) / 12M;
            string[] planes = 
            { 
                "A320", "A380", "A340", "A350", "A310", "Lockheed L-1011-200",
                "MAX", "A320-Neo", "777", "474-400", "474-8", "777-300" 
            };
            string plane = planes[new Random().Next(0, 10)];

            // Act
            AvailableFlight newFlight = new AvailableFlight(
                _identifierService.GetIdentifier(),
                flightData.Identifier,
                flightData.FromDestination.CityName, 
                flightData.ToDestination.CityName, 
                flightData.Departure,
                price,
                plane);
            try
            {
                _availableFlightRepository.Create(newFlight);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }
    }
}
