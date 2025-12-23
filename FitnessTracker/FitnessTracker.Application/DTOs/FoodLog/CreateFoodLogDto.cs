namespace FitnessTracker.Application.DTOs.FoodLog;

public class CreateFoodLogDto : FoodLogBase
{
    public int UserId { get; set; }
    public int FoodId { get; set; }
}
