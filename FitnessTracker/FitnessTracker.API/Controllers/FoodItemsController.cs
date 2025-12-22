using FitnessTracker.Application.DTOs.FoodItem;
using FitnessTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodItemsController : ControllerBase
{
    private readonly IFoodItemService _foodItemService;

    public FoodItemsController(IFoodItemService foodItemService)
    {
        _foodItemService = foodItemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetFoodItems()
    {
        var result = await _foodItemService.GetAllAsync();
        if (!result.Success) return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodItemDto>> GetFoodItem(int id)
    {
        var result = await _foodItemService.GetByIdAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Food item not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<FoodItemDto>> PostFoodItem(CreateFoodItemDto createDto)
    {
        var result = await _foodItemService.CreateAsync(createDto);
        if (!result.Success) return BadRequest(result.Error);

        var created = result.Data!;
        return CreatedAtAction(nameof(GetFoodItem), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFoodItem(int id, UpdateFoodItemDto updateDto)
    {
        var result = await _foodItemService.UpdateAsync(id, updateDto);

        if (!result.Success)
        {
            if (result.Error == "Food item not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFoodItem(int id)
    {
        var result = await _foodItemService.DeleteAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Food item not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
