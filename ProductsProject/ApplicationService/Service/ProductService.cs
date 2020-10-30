using Core.Entity;
using ProductsProject.Core.DomainService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductsProject.Core.ApplicationService.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _pRepos;
        public static IEnumerable<Product> productList;

        public ProductService(IProductRepository pRepos)
        {
            _pRepos = pRepos;
        }
        public Product ProductCreate(string name, double price, string color, string type, DateTime createdDate)
        {
            var product = new Product()
            {
                Name = name,
                Color = color,
                Type = type,
                CreatedDate = createdDate,
                Price = price
            };
            return product;
        }

        public Product Create(Product product)
        {
            if (product.Name == null || product.Name.Length < 1)
            {
                throw new InvalidDataException("You need to put in atleast 1 letter!");
            }
            return _pRepos.Create(product);
        }
        public Product Delete(int id)
        {
            if (id < 1)
            {

                throw new InvalidDataException("Id must be atleast 1");
            }
            return _pRepos.Delete(id);
        }

        public List<Product> GetProducts()
        {
            return _pRepos.GetAllProducts();
        }



        public Product ReadById(int id)
        {
            return _pRepos.GetProductById(id);
        }

        public Product Update(Product product)
        {
            if (product.Name.Length < 1)
            {
                throw new InvalidDataException("Name must be atleast 1 char");
            }

            if (product == null)
            {
                throw new InvalidDataException("Did not find avatar with id: " + product.id);
            }
            return _pRepos.Update(product);
        }


    }
}
