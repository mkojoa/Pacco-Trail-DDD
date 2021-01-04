using System;
using System.Collections.Generic;
using Convey.Types;

namespace Pacco.Services.Availability.Infrastructure.EfCore.Entities
{
    public class ResourceEntity : IIdentifiable<Guid>
    {
        public ResourceEntity()
        {
            Reservations = new HashSet<ReservationEntity>();
        }


        public Guid Id { get; set; }
        public int Version { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<ReservationEntity> Reservations { get; set; }
    }
}