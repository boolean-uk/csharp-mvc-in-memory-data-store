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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> Get()
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
        [Route("{id}")]
        public async Task<IResult> GetAProduct(int id)
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

        //update
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IResult> Update(int id, Product product)
        {
            //get item to update
            var item = _productRepository.find(id);

            if (item == null) return Results.NotFound();

            item.Name = string.IsNullOrEmpty(product.Name) ? item.Name : product.Name;
            item.Category = string.IsNullOrEmpty(product.Category) ? item.Category : product.Category;
            item.Price = product.Price;

            return Results.Ok(item);
        }

        //delete
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id}")]
        public async Task<IResult> DeleteAProduct(int id)
        {
            try
            {
                _productRepository.Delete(id);
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
