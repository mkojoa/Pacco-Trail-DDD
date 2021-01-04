using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacco.Services.Availability.Core.Events;

namespace Pacco.Services.Availability.Core.Entities
{
    /// <summary>
    /// Responsible for any change for the aggregate goes theought the root.
    /// </summary>
    public abstract class AggregateRoot
    {
        private readonly  List<IDomainEvent> _events = new List<IDomainEvent>();
        public AggregateId Id { get; protected set; }
        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events; 

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any())
            {
                Version++;
            }
            
            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();

    }
}
