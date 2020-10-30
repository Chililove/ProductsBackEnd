using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsProject.Core.DomainService
{
    public interface IProductRepository
    {

        IEnumerable<Product> ReadAllProducts();

        public List<Product> GetAllProducts();

        Product Create(Product product);

        Product GetProductById(int id);

        Product Update(Product updateProduct);

        Product Delete(int id);

    }
}
