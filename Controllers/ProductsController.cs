using Microsoft.AspNetCore.Mvc;
using FreakyFashionManager.Data;
using FreakyFashionManager.Models;
using System.Linq;

namespace FreakyFashionManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // Här kommer vi att lägga till fler metoder för CRUD-operationer
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.Id }, newProduct);
        }

        [HttpGet("{sku}")]
        public IActionResult GetProductBySku(string sku)
        {
            var product = _context.Products.FirstOrDefault(p => p.SKU == sku);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete("{sku}")]
        public IActionResult DeleteProduct(string sku)
        {
            var product = _context.Products.FirstOrDefault(p => p.SKU == sku);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
