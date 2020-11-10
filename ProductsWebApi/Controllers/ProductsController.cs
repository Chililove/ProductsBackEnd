using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsProject.Core.ApplicationService;
using ProductsProject.Core.DomainService;

namespace ProductsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {

            _productService = productService;
        }
        // GET: api/<ProductsController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Product> Get() => _productService.GetProducts();

        // GET api/<ProductsController>/5
       // [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return _productService.ReadById(id);
        }

        // POST api/<ProductsController>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            return _productService.Create(product);
        }

        // PUT api/<ProductsController>/5
      [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product product)
        {
            return _productService.Update(product);
        }

        // DELETE api/<ProductsController>/5
       [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            return _productService.Delete(id);
        }
    }
}
