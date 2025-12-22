namespace FitnessTracker.Application.DTOs.Workout;

public class WorkoutDto
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int DurationInMinutes { get; set; }

    public string? Notes { get; set; }

    public int UserId { get; set; }
}
