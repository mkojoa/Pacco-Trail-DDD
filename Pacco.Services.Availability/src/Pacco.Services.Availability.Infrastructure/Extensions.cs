using System;
using Convey;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Infrastructure.EfCore;
using Pacco.Services.Availability.Infrastructure.EfCore.Entities;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core;
using Pacco.Services.Availability.Infrastructure.Repositories;

namespace Pacco.Services.Availability.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            
            builder.Services.AddTransient<IResourcesRepository, ResourceRepository>();

            builder
               .AddQueryHandlers()
               .AddInMemoryQueryDispatcher()
               .AddEfCore<EfCoreContext>()
               .AddEfCoreRepository<ResourceEntity, Guid>();

            return builder;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            return app;
        }
    }
}