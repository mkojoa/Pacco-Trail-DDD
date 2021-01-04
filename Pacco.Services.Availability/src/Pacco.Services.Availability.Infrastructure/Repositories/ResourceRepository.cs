using System;
using System.Threading.Tasks;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Infrastructure.EfCore.Entities;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Services;

namespace Pacco.Services.Availability.Infrastructure.Repositories
{
    public class ResourceRepository : IResourcesRepository
    {
        private readonly IEfCoreRepository<ResourceEntity, Guid> _repository;

        public ResourceRepository(IEfCoreRepository<ResourceEntity, Guid> repository)
        {
            _repository = repository;
        }
        
        
        public async Task AddAsync(Resource resource)
        {
            await _repository.InsertRecordAsync(resource.AsEntity());

            await _repository.SaveAsync();
        }
        
        public async Task<Resource> GetAsync(AggregateId id)
        {
            var entity = await _repository.GetRecordsAsync(e => e.Id == id);
            await _repository.SaveAsync();
            return entity?.AsDtos();
        }
        
        public async Task UpdateAsync(Resource resource)
        {
            await _repository.UpdateRecordAsync(resource.AsEntity(),
                r => r.Id == resource.Id && r.Version < resource.Version);

            await _repository.SaveAsync();
        }


        public async Task DeleteAsync(AggregateId id)
        {
            await _repository.DeleteRecordAsync(id);
            await _repository.SaveAsync();
        }
    }
}