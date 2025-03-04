using CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // In-memory list to store products
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Sample Product 1", Price = 10.99M },
            new Product { Id = 2, Name = "Sample Product 2", Price = 15.99M }
        };

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            // Assign a new Id
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        public IActionResult Update(int id, Product product)
        {
            var productInDb = products.FirstOrDefault(p => p.Id == id);
            if (productInDb == null)
            {
                return NotFound();
            }
            productInDb.Name = product?.Name ?? productInDb.Name;
            productInDb.Price = product?.Price ?? productInDb.Price;

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            { 
                return NotFound(); 
            }

            products.Remove(product);
            return NoContent();
        }
    }
}
