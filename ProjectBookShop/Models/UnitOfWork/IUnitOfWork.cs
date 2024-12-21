using ProjectBookShop.Models.Repository;

namespace ProjectBookShop.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        
        BookShopContext _context { get; }
   
        IBookRepository BookRepository { get; }
        IBookRepository BookRepositoryy();
        IRepositoryBase<TEntity> Baserepository<TEntity>() where TEntity : class;
        Task commit();
    }
}
