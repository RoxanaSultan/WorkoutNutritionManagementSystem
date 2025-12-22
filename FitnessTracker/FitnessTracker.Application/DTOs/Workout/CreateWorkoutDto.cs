namespace FitnessTracker.Application.DTOs.Workout;

public class CreateWorkoutDto
{
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public string? Notes { get; set; }
    public int UserId { get; set; }
}
