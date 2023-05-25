using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using System;

namespace mvc_in_memory_data_store.Controllers
{
    [Route("bagels")]
    [ApiController]
    public class BagelsController : ControllerBase
    {
        IBagelRepository _bagelRepository;
        public BagelsController(IBagelRepository bagelRepository)
        {
            _bagelRepository = bagelRepository;
        }

        [HttpPost]
        public async Task<IResult> CreateBagel(string bagelType, int price)
        {
            try
            {
                var bagel = _bagelRepository.Create(bagelType, price);
                if (bagel != null)
                {
                    return Results.Created($"https://localhost:7241/bagels/{bagel.Id}",bagel);
                }
                else
                {
                    return Results.Problem("Not enough information in order to be created");
                }
            } catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                return Results.Ok(_bagelRepository.FindAll());
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
                var bagel = _bagelRepository.Find(id);
                return bagel != null ? Results.Ok(bagel) : Results.Problem($"There no bagel with id: {id}");
            } catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IResult> AddBagel(Bagel bagel)
        {
            try
            {
                if (_bagelRepository.Add(bagel)) return Results.Ok();
                return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IResult> UpdateBagel(int id, string? bagelType, int? price)
        {
            try
            {
                var bagel = _bagelRepository.Update(id, bagelType, price);
                return bagel != null ? Results.Ok(bagel) : Results.Problem($"There no bagel with id: {id}");
            } catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (_bagelRepository.Delete(id)) return Results.Ok();
                return Results.Problem($"There no bagel with id: {id}");

            } catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
