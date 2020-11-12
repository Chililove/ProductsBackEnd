using Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProductsProject.Core.ApplicationService;
using System;
using System.Collections.Generic;

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
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return _productService.ReadById(id);
        }

        // POST api/<ProductsController>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Product> Post([FromBody] JObject data)
        {
            var opb = new Product
            {
                Name = data["name"].ToString(),
                Color = data["color"].ToString(),
                Type = data["type"].ToString(),
                Price = Convert.ToDouble(data["price"].ToString()),
                CreatedDate = DateTime.Now,
                IsComplete = false
            };

            return _productService.Create(opb);
        }

        // PUT api/<ProductsController>/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] JObject data)
        {
            var opb = new Product
            {
                id = id,
                Name = data["name"].ToString(),
                Color = data["color"].ToString(),
                Type = data["type"].ToString(),
                Price = Convert.ToDouble(data["price"].ToString())
            };

            return _productService.Update(opb);
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
