using Microsoft.EntityFrameworkCore;
//using System.Data.Entity.Migrations;
using System.Linq.Expressions;

namespace ProjectBookShop.Models.Repository
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity> where TEntity : class where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected TContext _context { get; set; }

        private Microsoft.EntityFrameworkCore.DbSet<TEntity> dbset;

        public RepositoryBase(TContext context)
        {
            _context = context;
            dbset = _context.Set<TEntity>();

        }
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await dbset.ToListAsync();


            //return  dbset!=null?await dbset.ToListAsync() : null ;
        }
        public IEnumerable<TEntity> FindAll()
        {
            return dbset.AsNoTracking().ToList();
        }
        public async Task<TEntity> FindbyIdAsync(object id)
        {
            return await dbset.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> Filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbset;
            foreach (var include in includes)
            {
                query = query.Include(include);

            }
            if (Filter != null)
            {
                query = query.Where(Filter);
            }
            if (OrderBy != null)
            {
                query = OrderBy(query);
            }
            return await query.ToListAsync();
        }
        public async Task Create(TEntity entity)
        {

            await dbset.AddAsync(entity);
        }
        public void Update(TEntity entity) => dbset.Update(entity);
        public void Deleted(TEntity entity) => dbset.Remove(entity);
        public async Task CreateRange(IEnumerable<TEntity> entities) => await dbset.AddRangeAsync(entities);
        public void UpdateRange(IEnumerable<TEntity> entities) => dbset.UpdateRange(entities);
        public void DeletedRange(IEnumerable<TEntity> entities) => dbset.RemoveRange(entities);
        public bool AnyEntity(Expression<Func<TEntity, bool>> Any)
        {
            return dbset.Any<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetPaginteResualtAsync(int CurrentPage,int PageSize=1)
        {
            var entitys = await FindAllAsync();
          return  entitys.Skip((CurrentPage-1)*PageSize).Take(PageSize).ToList();

        }
        public int GetCount()
        {
            return dbset.Count();
        }

    }
}
