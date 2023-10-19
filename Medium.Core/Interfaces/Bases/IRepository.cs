using Medium.Core.Entities;
using System.Linq.Expressions;

namespace Medium.Core.Interfaces.Bases
{
    public interface IRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : Entity<TKey>
    {


        TEntity? GetById(TKey id);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<List<TEntity>> GETALL();

        List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        TEntity? GetFirst(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void InsertList(IQueryable<TEntity> entities);
        Task InsertListAsync(IQueryable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteList(IQueryable<TEntity> entities);
    }
}
