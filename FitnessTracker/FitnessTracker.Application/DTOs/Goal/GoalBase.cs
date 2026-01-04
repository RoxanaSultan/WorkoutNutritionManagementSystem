namespace FitnessTracker.Application.DTOs.Goal;

public class GoalBase
{
    public string GoalType { get; set; } = null!;
    public decimal TargetValue { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
