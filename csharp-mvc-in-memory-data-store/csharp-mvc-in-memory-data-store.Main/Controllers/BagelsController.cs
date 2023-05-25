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
    public class BagelsController : ControllerBase
    {
        IBagelRepository _bagelRepository;
        public BagelsController(IBagelRepository bagelRepository)
        {
            _bagelRepository = bagelRepository;
        }
        [HttpGet]
        public async Task<IResult> Get()
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
        [HttpGet]
        [Route("{id}")]
        public async Task<IResult> GetBagel(int id)
        {
            try
            {
                var bagel = _bagelRepository.find(id);
                return bagel.Id == id ? Results.Ok(bagel) : Results.NotFound($"Bagel withd id: {id} not found");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IResult> Put(int id, Bagel bagel)
        {
            try
            {
                var result = _bagelRepository.UpdateBagel(id, bagel);

                return result != null ? Results.Ok(result) : Results.NotFound($"Bagel withd id: {id} not found");
               

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> CreateBagel(Bagel bagel)
        {
            try
            {

                if(bagel == null) { return  Results.Problem("bagel is empty"); }
                 bagel.Id = _bagelRepository.findAll().Max(x=>x.Id) + 1;
                _bagelRepository.Add(bagel);
                // _bagelRepository.create(bagel.Type, bagel.Price);
                return Results.Created("ok", bagel);
            }catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IResult> DeleteBagel(int id)
        {
            try
            {
                var bagel = _bagelRepository.DeleteBagel(id);

                return bagel!=null ? Results.Ok(bagel) : Results.NotFound($"Bagel withd id: {id} not found");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }
    }
}
