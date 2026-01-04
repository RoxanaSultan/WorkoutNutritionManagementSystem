using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.Services;

public interface IExerciseService
{
    Task<Result<IEnumerable<ExerciseDto>>> GetAllAsync();
    Task<Result<ExerciseDto>> GetByIdAsync(int id);
    Task<Result<ExerciseDto>> CreateAsync(CreateExerciseDto dto);
    Task<Result<ExerciseDto>> UpdateAsync(int id, UpdateExerciseDto dto);
    Task<Result> DeleteAsync(int id);
}