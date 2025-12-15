namespace FitnessTracker.Domain.Entities;

public partial class FoodLog
{
    public int Id { get; set; }

    public DateTime LogDate { get; set; }

    public int Servings { get; set; }

    public decimal Quantity { get; set; }

    public int UserId { get; set; }

    public int FoodId { get; set; }

    public virtual FoodItem Food { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
