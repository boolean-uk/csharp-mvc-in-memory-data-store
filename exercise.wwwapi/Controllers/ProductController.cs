using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController(ProductContext context) : ControllerBase
    {
        private readonly ProductContext _context = context;
        
        [HttpGet]
        public IActionResult GetAllProducts() 
        {
            var products = _context.Products.ToList();
            if(products.Count == 0) return NotFound("No products of the provided category were found.");
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProduct(FrontProduct product) 
        {
            var products = _context.Products.ToList();
            var oldProduct = products.Find(x => x.Name == product.Name);
            if (oldProduct != null) return BadRequest("Product with provided name already exists");
           
            Product newProduct = new Product() { Name = product.Name, Category = product.Category, Price = product.Price };
            
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return Created($"http://localhost:5057/products/{newProduct.Id}", newProduct);
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var oldProduct = _context.Products.Find(product.Id);

            if (oldProduct == null) return NotFound("Product not found");
            if (oldProduct.Name.Equals(product.Name)) return BadRequest("Product with the provided name already exists.");
            
            _context.Products.Remove(oldProduct);
            _context.Products.Add(product);
            _context.SaveChanges();
            return Created($"http://localhost:5057/products/{product.Id}", product);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product is null) return NotFound("Product not found.");

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }
    }
}
