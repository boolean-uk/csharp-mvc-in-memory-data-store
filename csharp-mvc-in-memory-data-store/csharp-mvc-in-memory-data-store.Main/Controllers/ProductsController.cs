using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using System;

namespace mvc_in_memory_data_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get all of the products from list
        /// </summary>
        /// <returns>A list of all products</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("")]
        public async Task<IResult> GetProducts()
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

        /// <summary>
        /// Get a product by ID
        /// </summary>
        /// <param name="id">The ID of the product to get</param>
        /// <returns>Product with this id, if it exists, else 404 not found</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IResult> GetProduct(int id)
        {
            try
            {
                var product = _productRepository.FindById(id);
                return product != null ? Results.Ok(product) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResult> Put(Product product)
        {
            try
            {
                if (_productRepository.Add(product))
                    return Results.Ok();
                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
