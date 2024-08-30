using exercise.wwwapi.Models;
using exercise.wwwapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private ProductsService _productService;
        public ProductsController(ProductsService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create([FromBody] ProductDTO product)
        {
            if (product == null)
                return BadRequest("Product information is missing");

            var createdProduct = _productService.Create(product);

            if (createdProduct == null)
                return BadRequest("Unable to add product");

            var location = Url.Action("Get", new { id = createdProduct.Id });
            return Created(location, createdProduct);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetAll(string? category = null)
        {

            var products = _productService.GetAll(category);

            if (products == null)
                return NotFound("No products of the provided category were found");

            return Ok(products);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Get(int id)
        {

            var product = _productService.Get(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Update(int id, [FromBody] ProductDTO productDTO)
        {

            if (productDTO == null)
                return BadRequest("Product information is missing.");

            var updatedProduct = _productService.Update(id, productDTO);

            if (updatedProduct == null)
                return NotFound($"Unable to find product with id: {id} or name already exists");

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Delete(int id)
        {

            var deletedProduct = _productService.Delete(id);

            if (deletedProduct == null)
                return NotFound($"Unable to find product with id: {id}");

            return Ok(deletedProduct);
        }
    }
}
