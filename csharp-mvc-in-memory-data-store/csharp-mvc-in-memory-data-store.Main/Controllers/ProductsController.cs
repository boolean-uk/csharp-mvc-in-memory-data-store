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
        public async Task<IResult> CreateProduct(string name, string category, int price)
        {
            try
            {
                if (_productRepository.FindByName(name) != null)
                    return Results.BadRequest("Product with provided name already exists.");
                var product = _productRepository.Create(name, category, price);
                return Results.Created($"http://localhost:5186/Product/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
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
        /// Update an existing product by ID
        /// </summary>
        /// <param name="product">The fields of product to be updated</param>
        /// <returns>Updated product object</returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> UpdateProduct(int id, string name, string category, int price)
        {
            try
            {
                var product = _productRepository.FindById(id);
                if (product == null)
                    return Results.NotFound("Product not found.");
                if (_productRepository.FindByName(name) != null)
                    return Results.BadRequest("Product with provided name already exists.");
                
                if (!string.IsNullOrEmpty(name))
                    product.Name = name;
                if (!string.IsNullOrEmpty(category))
                    product.Category = category;
                product.Price = price;
                return Results.Created($"http://localhost:5186/Product/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Remove a product from list of products
        /// </summary>
        /// <param name="id">id of product to be deleted</param>
        /// <returns>Deleted product</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteProduct(int id)
        {
            try
            {
                var product = _productRepository.FindById(id);
                if (product == null)
                    return Results.NotFound("Product not found.");
                if (_productRepository.Delete(product))
                    Results.Ok(product);
                return Results.NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
