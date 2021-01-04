using System.Threading.Tasks;
using Pacco.Services.Availability.Core.Entities;

namespace Pacco.Services.Availability.Core.Repositories
{
    public interface IResourcesRepository
    {
        Task AddAsync(Resource resource);
        Task<Resource> GetAsync(AggregateId id);
        Task UpdateAsync(Resource resource);
        Task DeleteAsync(AggregateId id);
    }
}