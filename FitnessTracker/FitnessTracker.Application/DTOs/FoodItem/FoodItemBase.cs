namespace FitnessTracker.Application.DTOs.FoodItem;

public class FoodItemBase
{
    public string Name { get; set; } = null!;
    public int? Calories { get; set; }
    public decimal? Protein { get; set; }
    public decimal? Carbs { get; set; }
    public decimal? Fat { get; set; }
}
