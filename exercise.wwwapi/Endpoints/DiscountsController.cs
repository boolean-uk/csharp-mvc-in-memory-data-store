using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountsController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscounts()
        {
            var discounts = await _repository.GetDiscounts();
            if(discounts == null || !discounts.Any())
            {
                return NotFound(new { message = "No discounts were found." });
            }
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscount(int id)
        {
            var discount = await _repository.GetDiscount(id);
            if(discount == null)
            {
                return NotFound(new { message = "Discount not found." });
            }
            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(Discount discount)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid discount details." });
            }
            var createdDiscount = await _repository.AddDiscount(discount);
            return CreatedAtAction(nameof(GetDiscount) , new { id = createdDiscount.Id } , createdDiscount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id , Discount discount)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid discount details." });
            }
            var existingDiscount = await _repository.GetDiscount(id);
            if(existingDiscount == null)
            {
                return NotFound(new { message = "Discount not found." });
            }
            await _repository.UpdateDiscount(id , discount);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = await _repository.DeleteDiscount(id);
            if(discount == null)
            {
                return NotFound(new { message = "Discount not found." });
            }
            return Ok(discount);
        }
    }
}
