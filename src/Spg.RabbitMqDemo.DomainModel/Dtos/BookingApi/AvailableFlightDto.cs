using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.RabbitMqDemo.DomainModel.Dtos.BookingApi
{
    public record AvailableFlightDto(
        string Identifier,
        string FlightNumber,
        string FromDestination,
        string ToDestination,
        DateTime Departure,
        decimal BasePrice,
        string PlaneType);
}
