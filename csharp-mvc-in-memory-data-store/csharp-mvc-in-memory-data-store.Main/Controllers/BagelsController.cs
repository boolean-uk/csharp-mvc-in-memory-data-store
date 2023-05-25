using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using System;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace mvc_in_memory_data_store.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BagelsController : ControllerBase
    {
        IBagelRepository _bagelRepository;
        public BagelsController(IBagelRepository bagelRepository)
        {
            _bagelRepository = bagelRepository;
        }


        [HttpPost]
        public async Task<IResult> CreateBagel(string type, int price)
        {
            try
            {
                Bagel bagel = _bagelRepository.create(type, price);
                return Results.Created("ok", bagel);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("Bagels/{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                return Results.Ok(_bagelRepository.find(id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            try
            {
                return Results.Ok(_bagelRepository.findAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("Bagels/{id}")]
        public async Task<IResult> Put([Required] int id, string type, int price)
        {
            try
            {
                Bagel bagel = new Bagel(id, type, price);
                return Results.Ok(_bagelRepository.updateBagel(bagel));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Bagels/{id}")]
        public async Task<IResult> Delete([Required] int id)
        {
            try
            {
                return Results.Ok(_bagelRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
