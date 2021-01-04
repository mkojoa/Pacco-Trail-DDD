using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Pacco.Services.Availability.Application.Dtos;
using Pacco.Services.Availability.Application.Queries;
using Pacco.Services.Availability.Infrastructure.EfCore.Entities;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core;

namespace Pacco.Services.Availability.Infrastructure.EfCore.Queries.Handler
{
    public class GetResourcesHandler : IQueryHandler<GetResources, IEnumerable<ResourceDto>>
    {
        private readonly EfCoreContext _context;

        public GetResourcesHandler(EfCoreContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<ResourceDto>> HandleAsync(GetResources query)
        {
            var dbSet = _context.Set<ResourceEntity>();
            var collections = dbSet.Cast<ResourceEntity>();

            if (query.Tags is null || !query.Tags.Any())
            {
                var allEntities = await collections.Where(_ => true).ToListAsync();

                return allEntities.Select(d => d.AsDto());
            }

            var entities  = collections.AsQueryable();
            
            entities = query.MatchAllTags
                ? entities.Where(d => query.Tags.All(t => d.Tags.Contains(t)))
                : entities.Where(d => query.Tags.Any(t => d.Tags.Contains(t)));

            var resources = await entities.ToListAsync();

            return resources.Select(d => d.AsDto());
        }
    }
}