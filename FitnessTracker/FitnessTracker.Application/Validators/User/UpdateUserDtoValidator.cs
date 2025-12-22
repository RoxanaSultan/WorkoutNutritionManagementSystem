using FitnessTracker.Application.DTOs.User;
using FluentValidation;
namespace FitnessTracker.Application.Validators.User;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        Include(new CreateUserDtoValidator());
    }
}
