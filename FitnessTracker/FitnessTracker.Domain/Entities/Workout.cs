namespace FitnessTracker.Domain.Entities;

public partial class Workout
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int DurationInMinutes { get; set; }

    public string? Notes { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
}
