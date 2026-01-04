using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _workoutService;
    private readonly ILogger<WorkoutsController> _logger;

    public WorkoutsController(
        IWorkoutService workoutService,
        ILogger<WorkoutsController> logger)
    {
        _workoutService = workoutService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetWorkouts()
    {
        _logger.LogInformation("GET all workouts requested");

        var result = await _workoutService.GetAllAsync();

        if (!result.Success)
        {
            _logger.LogWarning("Failed to get workouts: {Error}", result.Error);
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Successfully returned {Count} workouts",
            result.Data?.Count() ?? 0);

        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutDto>> GetWorkout(int id)
    {
        _logger.LogInformation("GET workout by id={WorkoutId}", id);

        var result = await _workoutService.GetByIdAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Workout not found.")
            {
                _logger.LogWarning("Workout id={WorkoutId} not found", id);
                return NotFound(result.Error);
            }

            _logger.LogError("Error retrieving workout id={WorkoutId}: {Error}", id, result.Error);
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Workout id={WorkoutId} returned successfully", id);
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutDto>> PostWorkout(CreateWorkoutDto dto)
    {
        _logger.LogInformation("Creating workout on {Date} with duration {Duration} minutes",
            dto.Date, dto.DurationInMinutes);

        var result = await _workoutService.CreateAsync(dto);

        if (!result.Success)
        {
            _logger.LogError("Failed to create workout: {Error}", result.Error);
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Workout created with id={WorkoutId}", result.Data!.Id);

        return CreatedAtAction(nameof(GetWorkout),
            new { id = result.Data.Id },
            result.Data);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkout(int id, UpdateWorkoutDto dto)
    {
        _logger.LogInformation("Updating workout id={WorkoutId}", id);

        var result = await _workoutService.UpdateAsync(id, dto);

        if (!result.Success)
        {
            if (result.Error == "Workout not found.")
            {
                _logger.LogWarning("Cannot update workout id={WorkoutId} – not found", id);
                return NotFound(result.Error);
            }

            _logger.LogError("Failed to update workout id={WorkoutId}: {Error}", id, result.Error);
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Workout id={WorkoutId} updated successfully", id);
        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        _logger.LogInformation("Deleting workout id={WorkoutId}", id);

        var result = await _workoutService.DeleteAsync(id);

        if (!result.Success)
        {
            if (result.Error == "Workout not found.")
            {
                _logger.LogWarning("Cannot delete workout id={WorkoutId} – not found", id);
                return NotFound(result.Error);
            }

            _logger.LogError("Failed to delete workout id={WorkoutId}: {Error}", id, result.Error);
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Workout id={WorkoutId} deleted successfully", id);
        return NoContent();
    }

    [HttpGet("{workoutId}/exercises")]
    public async Task<ActionResult<IEnumerable<WorkoutExerciseDto>>> GetExercisesByWorkout(int workoutId)
    {
        _logger.LogInformation("Getting exercises for workout id={WorkoutId}", workoutId);

        var result = await _workoutService.GetExercisesByWorkout(workoutId);

        if (!result.Success)
        {
            _logger.LogWarning("Failed to get exercises for workout id={WorkoutId}: {Error}",
                workoutId, result.Error);

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
