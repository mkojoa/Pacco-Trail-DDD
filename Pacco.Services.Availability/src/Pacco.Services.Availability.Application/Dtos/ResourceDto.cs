﻿using System;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Application.Dtos
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<ReservationDto> Reservation { get; set; }
    }
}