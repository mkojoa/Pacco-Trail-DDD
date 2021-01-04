using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Types;
using Microsoft.EntityFrameworkCore;
using Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Helpers;
using Pacco.Services.Availability.Infrastructure.Exceptions;

namespace Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core.Services
{
    public class EfCoreRepository<TEntity, TIdentifiable> : IEfCoreRepository<TEntity, TIdentifiable>
        where TEntity : class, IIdentifiable<TIdentifiable>
    {
        private readonly EfCoreContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfCoreRepository(EfCoreContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _dbSet = context.Set<TEntity>();
        }
        
        /// <summary>
        /// No eager loading
        /// </summary>
        private IQueryable<TEntity> All => _dbSet.Cast<TEntity>();

        #region FIXME : DELETE
        // FIXME: Delete and use ALL instead.
        public IQueryable<TEntity> GetAllRecords() => _dbSet.AsQueryable();
        #endregion
        
        public Task<TEntity> GetRecordAsync(TIdentifiable id)
            => GetRecordsAsync(e => e.Id.Equals(id));

        public async Task<TEntity> GetRecordsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.Where(predicate).SingleOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query) where TQuery : IPagedQuery
            => _dbSet.AsQueryable().Where(predicate).PaginateAsync(query);

        // eager loading
        private IQueryable<TEntity> GetAllIncluding(
            params Expression<Func<TEntity, object>>[] includeProperties) =>
            includeProperties.Aggregate(All, (currentEntity, includeProperty) => currentEntity.Include(includeProperty));

        /// <summary>
        /// Takes in a lambda selector and let's you filter results from GetAllIncluding and All.
        /// </summary>
        /// <param name="selector">labmda expression to filter results by.</param>
        /// <param name="filteredSource">All or GetAllIncluding as the method to get results from.</param>
        /// <param name="includeProperties">array of eager load lamda expressions.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetFilteredRecord(
            Expression<Func<TEntity, bool>> selector, FilteredSource filteredSource,
            Expression<Func<TEntity, object>>[] includeProperties = null)
        {
            var results = default(IEnumerable<TEntity>);
            switch (filteredSource)
            {
                case FilteredSource.All:
                    results = All.Where(selector);
                    break;
                case FilteredSource.GetAllIncluding:
                    results = GetAllIncluding(includeProperties).Where(selector);
                    break;
            }
            return await (results ?? throw new ResourceNotFoundException()).AsQueryable().ToListAsync();
        }

        /// <summary>
        ///  Eager loading
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<TEntity> GetSingleRecordIncludingAsync(
            TIdentifiable id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = GetAllIncluding(includeProperties);
            //return await Filter<long>(entities, x => x.Id, id).FirstOrDefaultAsync();
            return await entities.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity> InsertRecordAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"No {nameof(TEntity)}  Entity was provided for Insert");
            }
            await _dbSet.AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateRecordAsync(TEntity entity)
            => UpdateRecordAsync(entity, predicate: e => e.Id.Equals(entity.Id));
        // {
        //     TEntity entityToUpdate = await
        //         _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(entity.Id));
        //     if (entityToUpdate == null)
        //     {
        //         //return null;
        //         throw new ArgumentNullException($"No {nameof(TEntity)}  Entity was provided for Update");
        //     }
        //
        //     _dbSet.Update(entity);
        //     return entity;
        // }

        public Task UpdateRecordAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
            =>  _dbSet.ReplaceOneAsync(predicate, entity);

        public Task DeleteRecordAsync(TIdentifiable id)
            => DeleteRecordAsync(e => e.Id.Equals(id));

        public async Task DeleteRecordAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.DeleteOneAsync(_dbSet, predicate);

        public Task<bool> ExistsRecordAsync(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.Where(predicate).AnyAsync();

        public Task SaveAsync() => _context.SaveChangesAsync();


    }
}