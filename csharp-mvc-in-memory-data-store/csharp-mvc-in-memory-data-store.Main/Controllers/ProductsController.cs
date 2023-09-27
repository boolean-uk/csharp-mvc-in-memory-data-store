using Microsoft.AspNetCore.Mvc;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState); // return validation errors
            }

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

        // GET /products to retrieve all products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get()
        {
            try
            {
                return Results.Ok(_productRepository.FindAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // GET /products/{id} to retrieve a specific product by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get(int id)
        {
            if (id <= 0)
            {
                return Results.BadRequest("Invalid Id provided.");
            }

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

        // PUT /products/{id} to update a product by ID
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Put(int id, [FromBody] Product updatedProduct)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            try
            {
                var product = _productRepository.Update(id, updatedProduct);
                if (product == null) return Results.NotFound();
                return Results.Ok(product);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // DELETE /products/{id} to delete a product by ID.
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Delete(int id)
        {
            if (id <= 0)
            {
                return Results.BadRequest("Invalid Id provided.");
            }

            try
            {
                var deletedProduct = _productRepository.Delete(id);
                if (deletedProduct != null)
                    return Results.Ok(deletedProduct);
                return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}