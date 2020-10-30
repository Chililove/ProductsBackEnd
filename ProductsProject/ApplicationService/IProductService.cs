using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsProject.Core.ApplicationService
{
   public interface IProductService
    {
        public List<Product> GetProducts();

        Product ProductCreate(string name, double price, string color, string type, DateTime createdDate);

        Product Create(Product product);

        Product Update(Product product);

        Product Delete(int id);

        Product ReadById(int id);
    }
}
