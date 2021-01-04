using System;

namespace Pacco.Services.Availability.Application.Dtos
{
    public class ReservationDto
    {
        public DateTime DateTime { get; set; }
        public int Priority { get; set; }
    }
}