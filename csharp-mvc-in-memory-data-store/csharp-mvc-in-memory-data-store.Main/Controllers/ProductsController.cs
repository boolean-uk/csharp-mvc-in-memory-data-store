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
        [Route("findAllProducts")]
        public async Task<IResult> GetProduct()
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("findAProduct/{Id}")]
        public async Task<IResult> GetAProduct(int Id)
        {
            try
            {
                return Results.Ok(_productRepository.find(Id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        
        [HttpPut]
        public async Task<IResult> Put(Product product)
        {
            try
            {
                if (_productRepository.Add(product)) return Results.Ok();
                return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("addProduct")]
        public async Task<IResult> Add(Product product)
        {
            if(product != null)
            {
                return _productRepository.Add(product) ? Results.Created($"https://localhost:7241/api/Products/addProduct", product) : Results.NotFound();
            }
            return Results.NotFound();
        }
    }
}
