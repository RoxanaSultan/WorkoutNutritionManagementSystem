namespace FitnessTracker.Application.DTOs.WorkoutExercise;

public class UpdateWorkoutExerciseDto
{
    public int? Sets { get; set; }
    public int? Reps { get; set; }
    public decimal? WeightUsed { get; set; }
}
