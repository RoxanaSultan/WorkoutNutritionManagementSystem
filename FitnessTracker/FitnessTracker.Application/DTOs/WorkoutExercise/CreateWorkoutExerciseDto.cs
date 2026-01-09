namespace FitnessTracker.Application.DTOs.WorkoutExercise;

public class CreateWorkoutExerciseDto : WorkoutExerciseBase
{
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
}
