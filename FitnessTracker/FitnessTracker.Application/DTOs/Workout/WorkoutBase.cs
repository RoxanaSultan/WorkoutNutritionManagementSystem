namespace FitnessTracker.Application.DTOs.Workout;

public class WorkoutBase
{
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public string? Notes { get; set; }
}
