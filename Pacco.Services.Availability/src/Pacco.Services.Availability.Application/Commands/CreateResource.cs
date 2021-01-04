using System;
using System.Collections.Generic;
using Convey.CQRS.Commands;

namespace Pacco.Services.Availability.Application.Commands
{
    public class CreateResource : ICommand
    {
        public Guid ResourceId { get; }
        public IEnumerable<string> Tags { get; }

        public CreateResource(Guid resourceId, IEnumerable<string> tags)
        {
            ResourceId = resourceId == Guid.Empty ? Guid.NewGuid() : resourceId;
            Tags = tags;
        }
    }
}