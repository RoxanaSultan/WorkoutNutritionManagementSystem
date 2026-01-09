using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.FoodLog;
using FitnessTracker.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

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
    public async Task<ActionResult<IEnumerable<FoodLogDto>>> GetFoodLogs()
    {
        var result = await _foodLogService.GetAllAsync();
        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    // GET: api/FoodLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FoodLogDto>> GetFoodLog(int id)
    {
        var result = await _foodLogService.GetByIdAsync(id);
        if (!result.Success)
        {
            if (result.Error == ErrorMessages.FoodLogNotFound)
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    // POST: api/FoodLogs
    [HttpPost]
    public async Task<ActionResult<FoodLogDto>> PostFoodLog(CreateFoodLogDto createDto)
    {
        var result = await _foodLogService.CreateAsync(createDto);
        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        var createdFoodLog = result.Data!;
        return CreatedAtAction(
            nameof(GetFoodLog),
            new { id = createdFoodLog.Id },
            createdFoodLog
            );
    }

    // PUT: api/FoodLogs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFoodLog(int id, UpdateFoodLogDto updateDto)
    {
        var result = await _foodLogService.UpdateAsync(id, updateDto);

        if (!result.Success)
        {
            if (result.Error == ErrorMessages.FoodLogNotFound)
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    // DELETE: api/FoodLogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFoodLog(int id)
    {
        var result = await _foodLogService.DeleteAsync(id);

        if (!result.Success)
        {
            if (result.Error == ErrorMessages.FoodLogNotFound)
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}