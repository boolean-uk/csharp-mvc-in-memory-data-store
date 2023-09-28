using Microsoft.AspNetCore.Mvc;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.DTOs;
using mvc_in_memory_data_store.Factories;
using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Controllers
{
    /// <summary>
    /// Products Controller to manage CRUD operations for Products
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFactory _productFactory;

        /// <summary>
        /// Next we initialize a new instance of the ProductsController class
        /// </summary>
        public ProductsController(IProductRepository productRepository, IProductFactory productFactory)
        {
            _productRepository = productRepository;
            _productFactory = productFactory;
        }

        /// <summary>
        /// Post to create a new product
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Post([FromBody] ProductInput productInput)
        {
            // Validation for the product input
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            // We check if product name allready exist
            if (_productRepository.ProductNameExists(productInput.Name))
            {
                return Results.BadRequest("A product with this name already exists.");
            }

            // Then we use the factory to convert the dto to the product model
            var product = _productFactory.CreateProduct(productInput);

            // Next we try to add the product and we return the result
            try
            {
                var createdProduct = _productRepository.Add(product);
                return Results.Created($"/products/{createdProduct.Id}", createdProduct);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Get to retrieve all products or filters by category.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get([FromQuery] string category = null)
        {
            try
            {
                var products = (category == null)
                    ? _productRepository.FindAll()
                    : _productRepository.FindByCategory(category);

                // To check if products exist in the given criteria
                if (products == null || products.Count == 0)
                    return Results.NotFound("No products of the provided category were found.");
                return Results.Ok(products);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Get to retrieve a specific product by its ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get(int id)
        {
            // We validate product ID
            if (id <= 0)
            {
                return Results.BadRequest("Invalid Id provided.");
            }

            // And then try to retrieve product and return the result
            try
            {
                var product = _productRepository.FindById(id);
                if (product == null) return Results.NotFound();
                return Results.Ok(product);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Put to update a product by its ID.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Put(int id, [FromBody] Product updatedProduct)
        {
            // We validate product ID and product details

            if (id <= 0 || !ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            // Then we fetch the existing product
            var existingProduct = _productRepository.FindById(id);
            if (existingProduct == null)
            {
                return Results.NotFound("Product not found.");
            }

            // Next wheck name conflict with other products
            if (existingProduct.Name != updatedProduct.Name && _productRepository.ProductNameExists(updatedProduct.Name))
            {
                return Results.BadRequest("A product with this name already exists.");
            }

            // And we try to update product and return result
            try
            {
                var product = _productRepository.Update(id, updatedProduct);
                if (product == null) return Results.NotFound("Failed to update product.");
                return Results.Created($"/products/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Delete(int id)
        {
            // We validate product ID
            if (id <= 0)
            {
                return Results.BadRequest("Invalid Id provided.");
            }

            // And then try to delete product and return result
            try
            {
                var deletedProduct = _productRepository.Delete(id);
                if (deletedProduct == null)
                    return Results.NotFound("Product not found.");
                return Results.Ok(deletedProduct);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}