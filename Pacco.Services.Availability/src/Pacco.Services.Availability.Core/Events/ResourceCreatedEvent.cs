using Pacco.Services.Availability.Core.Entities;

namespace Pacco.Services.Availability.Core.Events
{
    public class ResourceCreatedEvent : IDomainEvent
    {
        private Resource Resource;

        public ResourceCreatedEvent(Resource resource)
            => (Resource) = (resource);
    }
}