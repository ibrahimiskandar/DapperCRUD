using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly ContextDb db = new();

        // GET: api/<ProductsController>
        [HttpGet]

        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(db.GetProducts());
        }
        // GET api/<ProductsController>/5
        [HttpGet("Get")]
        public ActionResult<Product> GetProductById([FromQuery] int id)
        {
            return Ok(db.GetProduct(id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult AddProduct(ProductDTO product)
        {
            db.AddProduct(product);
            return Ok("Product Created");

        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult UptadeProduct(int id, [FromBody] ProductDTO product)
        {
            db.EditProduct(id, product);
            return Ok("Product Updated");

        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct( int id)
        {
            db.DeleteProduct(id);
            return Ok("Product Deleted");
        }
    }
}
