using FitnessTracker.Application.DTOs.Goal;
using FitnessTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalsController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    // GET: api/Goals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoals()
    {
        var result = await _goalService.GetAllAsync();
        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    // GET: api/Goals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GoalDto>> GetGoal(int id)
    {
        var result = await _goalService.GetByIdAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Goal not found.")
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    // PUT: api/Goals/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGoal(int id, UpdateGoalDto updateGoalDto)
    {
        var result = await _goalService.UpdateAsync(id, updateGoalDto);
        if (!result.Success)
        {
            if (result.Error == "Goal not found.")
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    // POST: api/Goals
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GoalDto>> PostGoal(CreateGoalDto createGoalDto)
    {
        var result = await _goalService.CreateAsync(createGoalDto);
        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        var createdGoal = result.Data;

        return CreatedAtAction(
            nameof(GetGoal),
            new { id = createdGoal!.Id },
            createdGoal
        );
    }

    // DELETE: api/Goals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var result = await _goalService.DeleteAsync(id);
        if (!result.Success)
        {
            if (result.Error == "Goal not found.")
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
