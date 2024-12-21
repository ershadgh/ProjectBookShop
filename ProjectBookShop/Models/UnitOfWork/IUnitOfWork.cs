using ProjectBookShop.Models.Repository;

namespace ProjectBookShop.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        
        BookShopContext _context { get; }
        IBookRepository _IBookRepository{get;}
        IBookRepository BookRepositoryy();
        IRepositoryBase<TEntity> Baserepository<TEntity>() where TEntity : class;
        Task commit();
    }
}
