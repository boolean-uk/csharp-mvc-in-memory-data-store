using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using System.Runtime.ConstrainedExecution;
using System;

namespace mvc_in_memory_data_store.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> GetAllProducts([FromQuery] string? category=null)
        {
            var products = _productRepository.GetAllProducts();

            if (string.IsNullOrEmpty(category))
            {
                products = _productRepository.GetAllProducts();
            }
            else
            {
                products = _productRepository.GetAllProducts()
                    .Where(p => string.Equals(p.category, category, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Results.Ok(products);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IResult> GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            return product != null ? Results.Ok(product) : Results.NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IResult> AddProduct(Product product)
        {
            var addedProduct = _productRepository.AddProduct(product);
            var uri = Url.Action(nameof(GetProductById), new { id = addedProduct.id }); 
            return Results.Created(uri, addedProduct);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IResult> DeleteProduct(int id)
        {
            var deletedProduct = _productRepository.DeleteProduct(id);
            if (deletedProduct == null) return Results.NotFound();
            return Results.Ok(deletedProduct);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IResult> UpdateProduct(int id, Product updatedProduct)
        {
            var product = _productRepository.UpdateProduct(id, updatedProduct);
            if (product == null) return Results.NotFound();
            return Results.Ok(product);
        }
    }
}