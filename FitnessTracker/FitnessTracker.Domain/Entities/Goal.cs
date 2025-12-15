namespace FitnessTracker.Domain.Entities;

public partial class Goal
{
    public int Id { get; set; }

    public string GoalType { get; set; } = null!;

    public decimal TargetValue { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool IsAchieved { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
