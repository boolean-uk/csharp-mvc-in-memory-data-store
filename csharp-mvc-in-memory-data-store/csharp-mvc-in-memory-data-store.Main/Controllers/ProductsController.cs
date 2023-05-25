using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using System;

namespace mvc_in_memory_data_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IResult> AddAProduct(Product product)
        {
            
            try
            {
                return _productRepository.AddProduct(product) ? Results.Created($"https://localhost:7241/Product{product.id}", product) : Results.Problem();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IResult> GetAllProducts()
        {
            try
            {                
                return Results.Ok(_productRepository.GetAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> GetAProduct(int id)
        {
            try
            {
                var result = _productRepository.GetById(id);
                return result!=null ? Results.Ok(result) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IResult> ChangeAProduct(int id,Product product)
        {
            try
            {
                return _productRepository.ChangeById(id, product)
                    ? Results.Created($"https://localhost:7241/Product{product.id}", product)
                    : Results.NotFound();

                //if (_productRepository.AddProduct(product)) return Results.Ok();
                //return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]

        public async Task<IResult> DeleteProduct(int id)
        {
            try
            {
                return _productRepository.RemoveById(id) ? Results.Ok() : Results.NotFound();
            //ok message is not yet working need to display item thats removed
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
