using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using mvc_in_memory_data_store.Models.Requests;

namespace mvc_in_memory_data_store.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Create(ProductRequest product)
        {
            try
            {
                Product newProduct = _productRepository.Create(product.Name, product.Category, product.Price);
                return Created("https://localhost:7241/Product", newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_productRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IResult Get(Guid id)
        {
            try
            {
                return Results.Ok(_productRepository.Get(id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public IResult Put(Guid Id, ProductRequest product) 
        {
            try
            {
               var newProduct = _productRepository.UpdateProduct(Id, product.Name, product.Category, product.Price);
                if (newProduct != null)
                {
                    return Results.Ok(newProduct);
                }
                return Results.NotFound();
                
            }
            catch (Exception ex) 
            { 
                return Results.Problem(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                if (_productRepository.Delete(Id))
                {
                    return Ok();

                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
