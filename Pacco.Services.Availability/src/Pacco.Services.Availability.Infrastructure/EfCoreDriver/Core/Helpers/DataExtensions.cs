using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Helpers
{
    public static class DataExtensions
    {
        public static Task ReplaceOneAsync<TEntity>(this DbSet<TEntity>             entities,
                                                    Expression<Func<TEntity, bool>> predicate, TEntity entity)
            where TEntity : class
        {
            var records = entities
                         .Where(predicate)
                         .SingleAsync();
            if (records != null)
                entities.UpdateRange(entity);
            
            return Task.CompletedTask;
        }
        
        public static Task DeleteOneAsync<TEntity>(this DbSet<TEntity>             entities,
                                                   object                          dbSet,
                                                   Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var records = entities
                         .Where(predicate)
                         .ToList();
            if (records.Count > 0)
                entities.RemoveRange(records);

            return Task.CompletedTask;
        }
    }
}