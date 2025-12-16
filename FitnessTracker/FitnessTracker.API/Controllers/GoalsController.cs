using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<Goal>>> GetGoals()
    {
        var result = await _goalService.GetAllAsync();

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    // GET: api/Goals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Goal>> GetGoal(int id)
    {
        var result = await _goalService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    // PUT: api/Goals/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGoal(int id, Goal goal)
    {
        var result = await _goalService.UpdateAsync(id, goal);

        if (!result.IsSuccess)
        {
            if (result.Error == "Goal not found.")
            {
                return NotFound(result.Error);
            }

            return BadRequest(result.Error);
        }

        return NoContent();
    }

    // POST: api/Goals
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Goal>> PostGoal(Goal goal)
    {
        var result = await _goalService.CreateAsync(goal);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(GetGoal),
            new { id = goal.Id },
            result.Value
        );
    }

    // DELETE: api/Goals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var result = await _goalService.DeleteAsync(id);

        if (!result.IsSuccess)
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
