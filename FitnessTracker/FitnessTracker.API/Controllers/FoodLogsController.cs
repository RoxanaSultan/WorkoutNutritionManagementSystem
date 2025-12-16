using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodLogsController : ControllerBase
    {
        private readonly IFoodLogService _foodLogService;

        public FoodLogsController(IFoodLogService foodLogService)
        {
            _foodLogService = foodLogService;
        }

        // GET: api/FoodLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodLog>>> GetFoodLogs()
        {
            var result = await _foodLogService.GetAllAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // GET: api/FoodLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodLog>> GetFoodLog(int id)
        {
            var result = await _foodLogService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // PUT: api/FoodLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodLog(int id, FoodLog foodLog)
        {
            var result = await _foodLogService.UpdateAsync(id, foodLog);

            if (!result.IsSuccess)
            {
                if (result.Error == "Food log not found.")
                {
                    return NotFound(result.Error);
                }

                return BadRequest(result.Error);
            }

            return NoContent();
        }

        // POST: api/FoodLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodLog>> PostFoodLog(FoodLog foodLog)
        {
            var result = await _foodLogService.CreateAsync(foodLog);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(
                nameof(GetFoodLog),
                new { id = foodLog.Id },
                result.Value
            );
        }

        // DELETE: api/FoodLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodLog(int id)
        {
            var result = await _foodLogService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Error == "Food log not found.")
                {
                    return NotFound(result.Error);
                }

                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
