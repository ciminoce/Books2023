﻿using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;

namespace Books2023.DataLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new CategoryRepository(_db);
            CoverTypes = new CoverTypeRepository(_db);
            Products=new ProductRepository(_db);
            Companies = new CompanyRepository(_db);
            ApplicationUsers=new ApplicationUserRepository(_db);
            ShoppingCarts=new ShoppingCartRepository(_db);
            OrderHeaders = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailRepository(_db);
        }

        public ICategoryRepository Categories { get; private set; }

        public ICoverTypeRepository CoverTypes { get; private set; }

        public IProductRepository Products {get;private set;}

        public ICompanyRepository Companies { get; private set; }

        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public IShoppingCartRepository ShoppingCarts {get;private set; }

		public IOrderHeaderRepository OrderHeaders { get; private set; }

		public IOrderDetailRepository OrderDetails { get; private set; }

		public void BeginTransaction()
		{
            _db.Database.BeginTransaction();
		}

		public void CommitTransaction()
		{
            _db.Database.CommitTransaction();
		}

		public void RollbackTransaction()
		{
            _db.Database.RollbackTransaction();
		}

		public void Save()
        {
           _db.SaveChanges();
        }
    }
}
