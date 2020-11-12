using Core.Entity;
using Microsoft.EntityFrameworkCore;
using ProductsProject.Core.DomainService;
using System.Collections.Generic;
using System.Linq;

namespace ProductsProject.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly Context _ctx;
        public static int ProductId = 1;

        private static List<Product> _productList = new List<Product>();

        public ProductRepository(Context ctx)
        {
            _ctx = ctx;
        }

        public Product Create(Product product)
        {
            Product pro = _ctx.Products.Add(product).Entity;
            _ctx.SaveChanges();
            return pro;
        }

        public Product Delete(int id)
        {
            Product prod = GetProductById(id);
            _ctx.Attach(prod).State = EntityState.Deleted;
            _ctx.SaveChanges();
            return prod;
            /*
            Product p = GetAllProducts().Find(x => x.id == id);
            GetAllProducts().Remove(p);
            if (p != null)
            {
                return p;
            }
            return null;*/
        }

        public List<Product> GetAllProducts()
        {
            return _ctx.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _ctx.Products.FirstOrDefault(pro => pro.id == id);
        }

        public IEnumerable<Product> ReadAllProducts()
        {
            return _productList;
        }

        public Product Update(Product updateProduct)
        {
            var product = GetProductById(updateProduct.id);
            if (product != null)
            {
                product.Name = updateProduct.Name;
                product.Price = updateProduct.Price;
                product.Color = updateProduct.Color;
                product.CreatedDate = updateProduct.CreatedDate;
                product.Type = updateProduct.Type;
            }
            _ctx.Attach(product).State = EntityState.Modified;
            _ctx.SaveChanges();
            return product;
        }
    }

}
