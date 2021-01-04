using System.Collections.Generic;
using System.Linq;
using Pacco.Services.Availability.Core.Events;
using Pacco.Services.Availability.Core.Exceptions;
using Pacco.Services.Availability.Core.ValueObjects;

namespace Pacco.Services.Availability.Core.Entities
{
    public class Resource : AggregateRoot
    {
        private ISet<string> _tags = new HashSet<string>();
        private ISet<Reservation> _reservations = new HashSet<Reservation>();

        /// <summary>
        /// Expose Tags
        /// </summary>
        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags = new HashSet<string>(value);
        }

        /// <summary>
        /// Expose the Reservations
        /// </summary>
        public IEnumerable<Reservation> Reservations
        {
            get => _reservations;
            private set => _reservations = new HashSet<Reservation>(value);
        }

        /// <summary>
        /// Set Tags, Reservations & Version
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tags"></param>
        /// <param name="reservations"></param>
        /// <param name="version"></param>
        public Resource(AggregateId id, IEnumerable<string> tags, IEnumerable<Reservation> reservations = null, int version = 0)
        {
            ValidateTags(tags);
            Id = id;
            Tags = tags;
            Reservations = reservations ?? Enumerable.Empty<Reservation>();
            Version = version;
        }

        /// <summary>
        /// Check if the resource has an Empty tags or invalid tags.
        /// </summary>
        /// <param name="tags"></param>
        /// <exception cref="MissingResourceTagsException"></exception>
        /// <exception cref="InvalidResourceTagsException"></exception>
        private static void ValidateTags(IEnumerable<string> tags)
        {
            // missing tags needed
            if (tags is null || !tags.Any())
            {
                throw new MissingResourceTagsException();
            }

            // there tags but some are null
            if (tags.Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidResourceTagsException();
            }
        }

        public static Resource Create(AggregateId id, IEnumerable<string> tags, IEnumerable<Reservation> reservations = null)
        {
            var resource = new Resource(id, tags, reservations);
            
            resource.AddEvent(new ResourceCreatedEvent(resource));

            return resource;
        }
    }
}