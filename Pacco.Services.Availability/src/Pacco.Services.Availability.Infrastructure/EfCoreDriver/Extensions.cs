using System;
using Convey;
using Convey.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Helpers;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Services;

namespace Pacco.Services.Availability.Infrastructure.EfCoreDriver
{
    internal static class Extensions
    {

        public static IConveyBuilder AddEfCore<TContext>(this IConveyBuilder builder)
            where TContext : DbContext
        {
            builder.Services.AddDbContext<TContext>(options =>
                options.UseSqlServer(
                    builder.GetOptions<EfCoreOptions>("EfCoreOptions").ConnectionString
                ));

            return builder;
        }
        
        public static IConveyBuilder AddEfCoreRepository<TEntity, TIdentifiable>(this IConveyBuilder builder)
            where TEntity : class, IIdentifiable<TIdentifiable>
        {
            builder
               .Services.AddScoped(typeof(IEfCoreRepository<,>), typeof(EfCoreRepository<,>));
            
            return builder;
        }
    }
}