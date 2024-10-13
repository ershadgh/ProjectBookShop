using System.Linq.Expressions;
using System.Security.Cryptography;

namespace ProjectBookShop.Models.Repository
{
    public interface IRepositoryBase<TEntity> 
    {
        Task<IEnumerable<TEntity>> FindAllAsync();
        IEnumerable<TEntity> FindAll();
        Task<TEntity> FindbyIdAsync(object id);
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> Filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy = null,params Expression<Func<TEntity, object>>[] includes);
        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Deleted(TEntity entity);
        Task CreateRange(IEnumerable<TEntity> entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void DeletedRange(IEnumerable<TEntity> entities);
        bool AnyEntity(Expression<Func<TEntity, bool>> Any);
        Task<IEnumerable<TEntity>> GetPaginteResualtAsync(int CurrentPage, int PageSize = 1);
        int GetCount();
    }
}
