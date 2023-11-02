using Medium.Core.Entities;
using System.Linq.Expressions;

namespace Medium.Core.Interfaces.Bases
{
    public interface IRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : Entity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAllAsync(int skip, int take, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? criteria, int? skip, int? take, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? criteria = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        Task InsertAsync(TEntity entity);
        Task InsertListAsync(IEnumerable<TEntity> entities);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? criteria = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? criteria = null);
        Task<TEntity?> FindAsync(params object[] id);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);





        TEntity? GetById(TKey id);
        TEntity? GetFirst(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        void Insert(TEntity entity);
        void InsertList(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteList(IQueryable<TEntity> entities);
    }
}
