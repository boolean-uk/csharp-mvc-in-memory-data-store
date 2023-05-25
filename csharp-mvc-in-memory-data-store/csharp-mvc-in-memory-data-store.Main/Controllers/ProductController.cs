using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using System;

namespace mvc_in_memory_data_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IResult> InsertProduct(string name, string category, decimal price)
        {
            try
            {
                if (name == null || category == null || price == 0) return Results.Problem();

                _productRepository.create(name, category, price);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IResult> GetProducts()
        {
            try
            {
                return Results.Ok(_productRepository.findAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> GetProduct(int id)
        {
            try
            {
                return Results.Ok(_productRepository.find(id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IResult> PutProduct(int id, string name, string category, decimal price)
        {
            try
            {
                var product = _productRepository.Update(id, name, category, price);
                if (product != null)
                {
                    return Results.Ok(product);
                }
                return Results.NotFound();
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
                if (_productRepository.Delete(id)) return Results.Ok();
                return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
