using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(string category)
        {
            var products = await _repository.GetProducts(category);
            if(products == null || !products.Any())
            {
                return NotFound(new { message = "No products of the provided category were found." });
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetProduct(id);
            if(product == null)
            {
                return NotFound(new { message = "Product not found." });
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Price must be an integer, something else was provided." });
            }
            var existingProduct = await _repository.GetProductByName(product.Name);
            if(existingProduct != null)
            {
                return BadRequest(new { message = "Product with provided name already exists." });
            }
            var createdProduct = await _repository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct) , new { id = createdProduct.Id } , createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id , Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Price must be an integer, something else was provided." });
            }
            var existingProduct = await _repository.GetProduct(id);
            if(existingProduct == null)
            {
                return NotFound(new { message = "Product not found." });
            }
            await _repository.UpdateProduct(id , product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.DeleteProduct(id);
            if(product == null)
            {
                return NotFound(new { message = "Product not found." });
            }
            return Ok(product);
        }
    }
}
