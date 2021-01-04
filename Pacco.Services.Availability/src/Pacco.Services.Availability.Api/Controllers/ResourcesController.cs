using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pacco.Services.Availability.Application.Commands;
using Pacco.Services.Availability.Application.Dtos;
using Pacco.Services.Availability.Application.Queries;

namespace Pacco.Services.Availability.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ResourcesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{resourceId}")]
        public async Task<ActionResult<ResourceDto>> GetResource([FromRoute]GetResource query)
        {
            var resource = await _queryDispatcher.QueryAsync(query);
            
            if (resource is {})
            {
                return resource;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostResource([FromBody] CreateResource command)
        {
            await _commandDispatcher.SendAsync(command);

            return Created($"api/resources/{command.ResourceId}", null);
        }
    }
}