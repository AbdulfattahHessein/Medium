using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Medium.DA.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medium.DA.Implementation.Bases
{
    public class Repository<TEntity, Key> : IRepository<TEntity, Key>
       where Key : struct
       where TEntity : Entity<Key>
    {
        #region Variables
        private readonly ApplicationDbContext _dbContext;

        protected readonly DbSet<TEntity> _table;
        #endregion

        #region CTOR
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity>();
        }

        #endregion

        #region Async Function
        public Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Includes(_table, includes);

            return query.ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(int skip, int take, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Includes(_table, includes);

            var items = await query.Skip(skip).Take(take).ToListAsync();

            return query.ToList();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? criteria, int? skip, int? take, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _table;

            if (criteria != null)
                query = query.Where(criteria);

            query = Includes(query, includes);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? criteria = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _table;

            if (criteria != null)
                query = query.Where(criteria);

            query = Includes(query, includes);

            return await query.ToListAsync();
        }
        public Task<TEntity?> GetByIdAsync(Key id)
        {
            return _table.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        public Task<TEntity?> GetByIdAsync(Key id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Includes(_table, includes);
            return query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
        public Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            return GetWhere(criteria, includes).FirstOrDefaultAsync();
        }
        public async Task InsertAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }
        public async Task InsertListAsync(IQueryable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? criteria = null)
        {
            return criteria == null ? await _table.AnyAsync() : await _table.AnyAsync(criteria);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? criteria = null)
        {
            return criteria == null ? await _table.CountAsync() : await _table.CountAsync(criteria);
        }

        #endregion

        #region Non Async Functions

        public TEntity? GetById(Key id)
        {
            return _table.SingleOrDefault(e => e.Id.Equals(id));
        }

        public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {

            var query = Includes(_table, includes);

            return query.ToList();
        }

        public TEntity? GetFirst(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            return GetWhere(criteria, includes).FirstOrDefault();
        }


        public void Insert(TEntity entity)
        {
            _table.Add(entity);
        }

        public void InsertList(IQueryable<TEntity> entities)
        {
            _table.AddRange(entities);
        }


        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void DeleteList(IQueryable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }
        #endregion

        #region Private functions

        private IQueryable<TEntity> Includes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query;
        }
        private IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Includes(_table, includes);

            return query.Where(criteria);
        }

        public async Task<TEntity?> FindAsync(params object[] id)
        {
            return await _table.FindAsync(id);
        }



        #endregion
    }

}
