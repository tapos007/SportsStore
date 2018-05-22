using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;

namespace SportsStore.Repositories
{
   public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }

    public class FakeProductRepository : IProductRepository
    {

        public IQueryable<Product> Products => new List<Product>
        {
            new Product {Name = "Football", Price = 25},
            new Product {Name = "Surf board", Price = 179},
            new Product {Name = "Running shoes", Price = 95}
        }.AsQueryable<Product>();
    }


    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _db;

        public EFProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Product> Products => _db.Products;
    }
}
