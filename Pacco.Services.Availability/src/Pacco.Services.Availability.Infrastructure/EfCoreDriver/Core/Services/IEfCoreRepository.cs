using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Types;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Helpers;

namespace Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Services
{
    public interface IEfCoreRepository<TEntity, in TIdentifiable>
        where TEntity : IIdentifiable<TIdentifiable>
    {
        
        /// <summary>
        /// Get Filtered Record
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="filteredSource"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetFilteredRecord(
            Expression<Func<TEntity, bool>> selector, FilteredSource filteredSource,
            Expression<Func<TEntity, object>>[] includeProperties = null);

        /// <summary>
        /// Eager loading
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<TEntity> GetSingleRecordIncludingAsync(TIdentifiable id, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Insert new record by passing the entity to the method.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>returns list of inserted record</returns>
        Task<TEntity> InsertRecordAsync(TEntity entity);
        
        /// <summary>
        /// Get all Queryable collection
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAllRecords();
        
        /// <summary>
        /// Get a Single Record by RecordId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetRecordAsync(TIdentifiable id);
        
        /// <summary>
        /// Fetch all record with predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetRecordsAsync(Expression<Func<TEntity, bool>>  predicate);
        
        /// <summary>
        /// Find Record using read only list
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// Browse record and paginate them
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="query"></param>
        /// <typeparam name="TQuery"></typeparam>
        /// <returns></returns>
        Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
            TQuery query) where TQuery : IPagedQuery;

        /// <summary>
        /// Update Record by Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateRecordAsync(TEntity entity);
        
        /// <summary>
        /// Update record with predicate
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task UpdateRecordAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// Delete record by record id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRecordAsync(TIdentifiable id);
        
        /// <summary>
        /// Delete record by predicate filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task DeleteRecordAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// Check if exist record with predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> ExistsRecordAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// Save Record
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}