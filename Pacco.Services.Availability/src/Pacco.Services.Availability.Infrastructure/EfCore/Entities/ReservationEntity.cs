using System;
using System.Collections.Generic;
using Convey.Types;

namespace Pacco.Services.Availability.Infrastructure.EfCore.Entities
{
    public class ReservationEntity : IIdentifiable<Guid>
    {

        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Priority { get; set; }
        public Guid ResourceId { get; set; }
        public int TimeStamp { get; set; }
    }
}