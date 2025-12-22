using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    public WorkoutsController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetWorkouts()
    {
        var result = await _workoutService.GetAllAsync();
        if (!result.Success) return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutDto>> GetWorkout(int id)
    {
        var result = await _workoutService.GetByIdAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Workout not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutDto>> PostWorkout(CreateWorkoutDto dto)
    {
        var result = await _workoutService.CreateAsync(dto);

        if (!result.Success)
            return BadRequest(result.Error);

        var created = result.Data!;
        return CreatedAtAction(nameof(GetWorkout), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkout(int id, UpdateWorkoutDto dto)
    {
        var result = await _workoutService.UpdateAsync(id, dto);

        if (!result.Success)
        {
            if (result.Error == "Workout not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        var result = await _workoutService.DeleteAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Workout not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpGet("{workoutId}/exercises")]
    public async Task<ActionResult<IEnumerable<WorkoutExerciseDto>>> GetExercisesByWorkout(int workoutId)
    {
        var result = await _workoutService.GetExercisesByWorkout(workoutId);

        if (!result.Success)
        {
            if (result.Error == "Workout not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }


    [HttpPost("{workoutId}/exercises")]
    public async Task<IActionResult> AddExercise(int workoutId, CreateWorkoutExerciseDto dto)
    {
        var result = await _workoutService.AddExerciseAsync(workoutId, dto);
        if (!result.Success) return BadRequest(result.Error);
        return Ok();
    }

    [HttpPut("{workoutId}/exercises/{exerciseId}")]
    public async Task<IActionResult> UpdateExercise(int workoutId, int exerciseId, UpdateWorkoutExerciseDto dto)
    {
        var result = await _workoutService.UpdateExerciseAsync(workoutId, exerciseId, dto);
        if (!result.Success)
        {
            if (result.Error == "Workout exercise not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpDelete("{workoutId}/exercises/{exerciseId}")]
    public async Task<IActionResult> RemoveExercise(int workoutId, int exerciseId)
    {
        var result = await _workoutService.RemoveExerciseAsync(workoutId, exerciseId);
        if (!result.Success)
        {
            if (result.Error == "Workout exercise not found.")
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
