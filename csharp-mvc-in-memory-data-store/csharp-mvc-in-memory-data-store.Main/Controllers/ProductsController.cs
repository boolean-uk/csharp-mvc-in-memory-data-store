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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> GetAllProducts([FromQuery] string? category = null)
        {
            List<Product> products;

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

            if (products.Count == 0 && !string.IsNullOrEmpty(category))
            {
                return Results.NotFound(new { Message = $"No products of the provided category '{category}' were found!" });
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

            return product != null ? Results.Ok(new { Message = "Specific product returned!", product }) : Results.NotFound(new { Message = "Product not found!" });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> AddProduct(Product product)
        {
            if (_productRepository.GetAllProducts().Any(p => p.name == product.name))
            {
                return Results.BadRequest(new { Message = "Product with provided name already exists!" });
            }

            var addedProduct = _productRepository.AddProduct(product);
            var uri = Url.Action(nameof(GetProductById), new { id = addedProduct.id });

            var response = new
            {
                Message = "Product added successfully!",
                Product = addedProduct,
                Uri = uri
            };

            return Results.Created(uri, response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IResult> DeleteProduct(int id)
        {
            var deletedProduct = _productRepository.DeleteProduct(id);

            if (deletedProduct == null) return Results.NotFound(new { Message = "Product not found!" });

            return Results.Ok(new { Message = "Product deleted successfully!", deletedProduct });
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IResult> UpdateProduct(int id, Product updatedProduct)
        {
            if (_productRepository.GetAllProducts().Any(p => p.name == updatedProduct.name && p.id != id))
            {
                return Results.BadRequest(new { Message = "Product with provided name already exists!" });
            }

            var product = _productRepository.UpdateProduct(id, updatedProduct);

            if (product == null)
            {
                return Results.NotFound(new { Message = "Product not found!" });
            }

            return Results.Ok(new { Message = "Product updated successfully!", product });
        }
    }
}