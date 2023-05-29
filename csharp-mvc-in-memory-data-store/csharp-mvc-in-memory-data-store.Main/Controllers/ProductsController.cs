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
        IProductRepository _productReposetory;
        public ProductsController(IProductRepository productReposetory)
        {
            _productReposetory = productReposetory;
        }

        [HttpGet]
        public async Task<IResult> GetProducts()
        {
            try
            {
                return Results.Ok(_productReposetory.GetProducts());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IResult> AddAProduct(Product product)
        {
            try
            {
                return _productReposetory.AddProduct(product) ? Results.Created($"yeah", product) : Results.Problem();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> GetAProductById(int id)
        {
            try
            {
                var product = _productReposetory.GetProductById(id);
                return product!=null ? Results.Ok(product) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IResult> UpdateAProduct(Product product, int id)
        {
            try
            {
                return _productReposetory.UpdateProduct(product, id)
                    ? Results.Created($"yes", product)
                    : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IResult> DeleteAProduct(int id)
        {
            try
            {
                var product = GetAProductById(id);
                return _productReposetory.DeleteProduct(id) ? Results.Ok(product) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


    }
}
