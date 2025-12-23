namespace FitnessTracker.Application.DTOs.FoodLog;

public class FoodLogBase
{
    public DateTime LogDate { get; set; }
    public int Servings { get; set; }
    public decimal Quantity { get; set; }
}
