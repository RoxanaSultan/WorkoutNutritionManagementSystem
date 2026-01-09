using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IWorkoutExerciseService _workoutExerciseService;

        public WorkoutExercisesController(IWorkoutExerciseService workoutExerciseService)
        {
            _workoutExerciseService = workoutExerciseService;
        }

        // GET: api/WorkoutExercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutExercise>>> GetWorkoutExercises()
        {
            var result = await _workoutExerciseService.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        // GET: api/WorkoutExercises/5
        [HttpGet("{workoutId}/{exerciseId}")]
        public async Task<ActionResult<WorkoutExercise>> GetWorkoutExercise(int workoutId, int exerciseId)
        {
            var result = await _workoutExerciseService.GetByIdAsync(workoutId, exerciseId);

            if (!result.Success)
            {
                if (result.Error == ErrorMessages.WorkoutExerciseNotFound)
                {
                    return NotFound(result.Error);
                }

                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        // PUT: api/WorkoutExercises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{workoutId}/{exerciseId}")]
        public async Task<IActionResult> PutWorkoutExercise(int workoutId, int exerciseId, UpdateWorkoutExerciseDto workoutExercise)
        {
            var result = await _workoutExerciseService.UpdateAsync(workoutId, exerciseId, workoutExercise);

            if (!result.Success)
            {
                if (result.Error == ErrorMessages.WorkoutExerciseNotFound)
                {
                    return NotFound(result.Error);
                }

                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        // POST: api/WorkoutExercises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateWorkoutExerciseDto>> PostWorkoutExercise(CreateWorkoutExerciseDto workoutExerciseDto)
        {
            var result = await _workoutExerciseService.CreateAsync(workoutExerciseDto);

            if (!result.Success)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetWorkoutExercise),
                new { workoutId = result.Data!.WorkoutId, exerciseId = result.Data!.ExerciseId },
                result.Data);
        }

        // DELETE: api/WorkoutExercises/5
        [HttpDelete("{workoutId}/{exerciseId}")]
        public async Task<IActionResult> DeleteWorkoutExercise(int workoutId, int exerciseId)
        {
            var result = await _workoutExerciseService.DeleteAsync(workoutId, exerciseId);

            if (!result.Success)
            {
                if (result.Error == ErrorMessages.WorkoutExerciseNotFound)
                {
                    return NotFound(result.Error);
                }

                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
