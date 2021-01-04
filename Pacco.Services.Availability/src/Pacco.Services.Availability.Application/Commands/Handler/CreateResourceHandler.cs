using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Repositories;

namespace Pacco.Services.Availability.Application.Commands.Handler
{
    public class CreateResourceHandler : ICommandHandler<CreateResource>
    {
        private readonly IResourcesRepository _resourceRepository;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourcesRepository"></param>
        public CreateResourceHandler(IResourcesRepository resourcesRepository)
        {
            _resourceRepository = resourcesRepository;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="ResourceAlreadyExistsException"></exception>
        public async Task HandleAsync(CreateResource command)
        {
            //check if resource exist
            var resource = await _resourceRepository.GetAsync(command.ResourceId);
            if (resource is {})
            {
                throw new ResourceAlreadyExistsException(command.ResourceId);
            }

            // add new resource.
            resource = Resource.Create(command.ResourceId, command.Tags);
            await _resourceRepository.AddAsync(resource);
        }
    }
}