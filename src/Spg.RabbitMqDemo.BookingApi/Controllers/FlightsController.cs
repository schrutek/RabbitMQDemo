using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.RabbitMqDemo.Application.BookingApi;
using Spg.RabbitMqDemo.DomainModel.Dtos.BookingApi;
using Spg.RabbitMqDemo.DomainModel.Interfaces;
using Spg.RabbitMqDemo.Repository;

namespace Spg.RabbitMqDemo.BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IAvailableFlightService _flightService;

        public FlightsController(IAvailableFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_flightService
                .GetAll()
                .Select(f => 
                    new AvailableFlightDto(
                        f.Identifier, 
                        f.FlightNumber, 
                        f.FromDestination, 
                        f.ToDestination,
                        f.Departure, 
                        f.BasePrice, 
                        f.PlaneType
                    )
                )
            );
        }
    }
}
