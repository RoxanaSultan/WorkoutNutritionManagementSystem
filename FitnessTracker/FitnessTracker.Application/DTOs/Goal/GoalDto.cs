namespace FitnessTracker.Application.DTOs.Goal;

public class GoalDto : GoalBase
{
    public int Id { get; set; }
    public bool IsAchieved { get; set; }
    public int UserId { get; set; }
}
