using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Pacco.Services.Availability.Application.Dtos;
using Pacco.Services.Availability.Application.Queries;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Infrastructure.EfCore.Entities;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core;

namespace Pacco.Services.Availability.Infrastructure.EfCore.Queries.Handler
{
    public class GetResourceHandler : IQueryHandler<GetResource, ResourceDto>
    {
        private readonly EfCoreContext _context;

        public GetResourceHandler(EfCoreContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        
        public async Task<ResourceDto> HandleAsync(GetResource query)
        {
            var entity = await _context.Set<ResourceEntity>()
                                       .Where(r => r.Id == query.ResourceId)
                                       .SingleOrDefaultAsync();

            return entity?.AsDto();
        }
    }
}