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
        /// Add a new product to list of available products
        /// </summary>
        /// <param name="product">New product</param>
        /// <returns>201 response or error</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateProduct(Product product)
        {
            if (!(product.Price is int))
            {
                return Results.BadRequest("Price must be an integer, something else was provided.");
            }
            if (_productRepository.FindByName(product.Name) != null)
            {
                return Results.BadRequest("Product with provided name already exists.");
            }
            if (_productRepository.Add(product))
                return Results.Created($"http://localhost:5186/Product/{product.Id}", product);
            return Results.BadRequest("Could not create product");
        }

        /// <summary>
        /// Get all of the products from list
        /// </summary>
        /// <returns>A list of all products</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> GetProduct(int id)
        {
            try
            {
                var product = _productRepository.FindById(id);
                return product != null ? Results.Ok(product) : Results.NotFound("Product not found.");
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
