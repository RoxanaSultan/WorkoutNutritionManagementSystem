using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Application.DTOs.User;

public class UserDto : UserBase
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string? Gender { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public DateTime RegistrationDate { get; set; }
}
