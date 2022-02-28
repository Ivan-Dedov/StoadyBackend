using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

namespace Stoady.DataAccess.Repositories
{
    public sealed class DatabaseRepository : IDatabaseRepository
    {
        private readonly StoadyDataContext _dataContext;

        public DatabaseRepository(StoadyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <inheritdoc/>
        public IQueryable<T> Query<T>() where T : class, IDao
        {
            return _dataContext
                .Set<T>()
                .AsQueryable();
        }

        /// <inheritdoc/>
        public IQueryable<T> Query<T>(Expression<Func<T, bool>> selector) where T : class, IDao
        {
            return _dataContext
                .Set<T>()
                .Where(selector)
                .AsQueryable();
        }

        /// <inheritdoc/>
        public async Task<long> Add<T>(T item) where T : class, IDao
        {
            var entity = await _dataContext.Set<T>().AddAsync(item);
            return entity.Entity.Id;
        }

        /// <inheritdoc/>
        public async Task AddRange<T>(IEnumerable<T> items) where T : class, IDao
        {
            await _dataContext.Set<T>().AddRangeAsync(items);
        }

        /// <inheritdoc/>
        public void Remove<T>(long id) where T : class, IDao
        {
            var item = Query<T>(x => x.Id == id).First();
            _dataContext.Set<T>().Remove(item);
        }

        /// <inheritdoc/>
        public void Remove<T>(T item) where T : class, IDao
        {
            _dataContext.Set<T>().Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveRange<T>(IEnumerable<T> items) where T : class, IDao
        {
            _dataContext.Set<T>().RemoveRange(items);
        }

        /// <inheritdoc/>
        public void Update<T>(T item) where T : class, IDao
        {
            _dataContext.Set<T>().Update(item);
        }

        /// <inheritdoc/>
        public void UpdateRange<T>(IEnumerable<T> items) where T : class, IDao
        {
            _dataContext.Set<T>().UpdateRange(items);
        }

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
