namespace FitnessTracker.Application.DTOs.FoodLog;

public class FoodLogDto : FoodLogBase
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FoodId { get; set; }
}
