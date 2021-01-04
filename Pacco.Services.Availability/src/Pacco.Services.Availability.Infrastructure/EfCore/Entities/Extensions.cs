using System;
using System.Linq;
using Pacco.Services.Availability.Application.Dtos;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.ValueObjects;

namespace Pacco.Services.Availability.Infrastructure.EfCore.Entities
{
    internal static class Extensions
    {
        public static Resource AsDtos(this ResourceEntity entity)
            => new Resource(entity.Id, entity.Tags, entity.Reservations?.Select(r=>new Reservation(r.TimeStamp.AsDateTime(), r.Priority)));

        public static ResourceEntity AsEntity(this Resource entity)
        => new ResourceEntity
           {
               Id = entity.Id,
               Version = entity.Version,
               Tags = entity.Tags,
               Reservations = entity.Reservations.Select(r => new ReservationEntity
                                                              {
                                                                  TimeStamp = r.DateTime.AsDaySinceEpoch(),
                                                                  Priority = r.Priority
                                                              })
           };
        
        public static int AsDaySinceEpoch(this DateTime dateTime)
            => (dateTime - new DateTime()).Days;
        
        public static DateTime AsDateTime(this int daysSinceEpoch)
        => new DateTime().AddDays((daysSinceEpoch));
        
        public  static  ResourceDto AsDto(this ResourceEntity entities)
        => new ResourceDto
        {
            Id = entities.Id,
            Tags = entities.Tags,
            Reservation = entities.Reservations?.Select(r => new ReservationDto
            {
                Priority = r.Priority,
                DateTime = r.TimeStamp.AsDateTime()
            })
        };
    }
}