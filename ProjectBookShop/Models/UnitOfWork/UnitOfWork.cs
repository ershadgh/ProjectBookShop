﻿using Microsoft.EntityFrameworkCore;
using ProjectBookShop.classes;
using ProjectBookShop.Models.Repository;

namespace ProjectBookShop.Models.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IConvertDate _convertDate;
        private IBookRepository _IBooksRepository;

        public  BookShopContext _context { get; }
      
        public UnitOfWork(BookShopContext context,IConvertDate convertDate)
        {
            _context = context;
            _convertDate = convertDate;
        }
        public IBookRepository BookRepositoryy()
        {
             IBookRepository rep = new BookRepository(_context,_convertDate,this);
            return rep;
        }
        public IRepositoryBase<TEntity>  Baserepository<TEntity>() where TEntity:class
        {
            IRepositoryBase<TEntity> repositoryBase = new RepositoryBase<TEntity, BookShopContext>(_context);
            return repositoryBase;
        }

        public IBookRepository BookRepository
        {

            get
            {
                if (_IBooksRepository == null)
                {
                    _IBooksRepository = new BookRepository(_context, _convertDate, this);
                }
                return _IBooksRepository;
            }
            
        }

        

        public async Task commit()
        {
            _context.SaveChanges();
        }
    }
}
