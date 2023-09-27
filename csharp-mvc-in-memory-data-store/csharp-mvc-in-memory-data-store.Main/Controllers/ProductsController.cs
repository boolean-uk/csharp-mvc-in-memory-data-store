using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using mvc_in_memory_data_store.Models;
using mvc_in_memory_data_store.Repositories;
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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("findAllProducts")]
        public async Task<IResult> GetProduct()
        {
            try
            {                
                return Results.Ok(_productRepository.findAll());
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("findAProduct/{Id}")]
        public async Task<IResult> GetAProduct(int Id)
        {
            try
            {
                return Results.Ok(_productRepository.find(Id));
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addProduct")]
        public async Task<IResult> Add(Product product)
        {
            if(product != null)
            {
                return _productRepository.Add(product) ? Results.Created($"https://localhost:7241/api/Products/addProduct", product) : Results.NotFound();
            }
            return Results.BadRequest("Not Found");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("updateProduct/{Id}")]
        public async Task<IResult> UpdateProduct(int Id, Product product)
        {
            try
            {
                if(_productRepository.UpdateProduct(Id, product)) {
                    return Results.Created($"https://localhost:7241/api/Products/updateProduct", product);
                }
                return Results.NotFound("Not Found");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("deleteProduct/{id}")]
        public async Task<IResult> DeleteProduct(int id)
        {
            try
            {
                if (_productRepository.DeleteProduct(id))
                {
                    return Results.Ok();
                }
                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }

        }
    }
}
