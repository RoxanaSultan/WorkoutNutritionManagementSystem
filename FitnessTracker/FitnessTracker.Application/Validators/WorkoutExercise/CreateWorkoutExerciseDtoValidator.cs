//using FluentValidation;
//using FitnessTracker.Application.DTOs.WorkoutExercise;

//namespace FitnessTracker.Application.Validators.WorkoutExercise;

//public class CreateWorkoutExerciseDtoValidator : AbstractValidator<CreateWorkoutExerciseDto>
//{
//    public CreateWorkoutExerciseDtoValidator()
//    {
//        RuleFor(x => x.WorkoutId).GreaterThan(0);
//        RuleFor(x => x.ExerciseId).GreaterThan(0);

//        RuleFor(x => x.Sets)
//            .GreaterThan(0).WithMessage("Sets must be a positive value.")
//            .When(x => x.Sets.HasValue);

//        RuleFor(x => x.Reps)
//            .GreaterThan(0).WithMessage("Reps must be a positive value.")
//            .When(x => x.Reps.HasValue);

//        RuleFor(x => x.WeightUsed)
//            .GreaterThanOrEqualTo(0).WithMessage("WeightUsed cannot be negative.")
//            .When(x => x.WeightUsed.HasValue);
//    }
//}