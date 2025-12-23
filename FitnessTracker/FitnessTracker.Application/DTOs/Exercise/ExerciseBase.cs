namespace FitnessTracker.Application.DTOs.Exercise;

public class ExerciseBase
{
    public string Name { get; set; } = null!;
    public string? MuscleGroup { get; set; }
    public string? DifficultyLevel { get; set; }
}
