namespace FitnessTracker.Application.DTOs.FoodLog;

public class FoodLogDto
{
    public int Id { get; set; }
    public DateTime LogDate { get; set; }
    public int Servings { get; set; }
    public decimal Quantity { get; set; }
    public int UserId { get; set; }
    public int FoodId { get; set; }
}
