namespace FitnessTracker.Application.DTOs.MeasurementLog;

public class MeasurementLogDto
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public decimal Weight { get; set; }

    public decimal BodyFatPercentage { get; set; }

    public decimal Waist { get; set; }

    public decimal Chest { get; set; }

    public decimal Arms { get; set; }

    public int UserId { get; set; }
}
