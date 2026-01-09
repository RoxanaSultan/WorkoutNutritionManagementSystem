namespace FitnessTracker.Application.Common;

public static class ErrorMessages
{
    // User
    public const string UserNotFound = "User not found.";
    public const string UserEmailAlreadyExists = "Email already exists.";
    public static string UserWithIdNotFound(int id) => $"User with id {id} not found.";

    public const string UserNameRequired = "Name cannot be empty.";
    public const string UserNameMaxLength50 = "Name must have maximum 50 characters.";

    public const string UserEmailRequired = "Email cannot be empty.";
    public const string UserEmailInvalid = "Email must be in a valid format.";

    public const string UserBirthDateRequired = "BirthDate cannot be empty.";
    public const string UserBirthDateInPast = "BirthDate must be in the past.";
    public const string UserTooYoung = "User must be at least 13 years old.";

    public const string UserHeightRange = "Height must be between 50 cm and 250 cm.";
    public const string UserWeightRange = "Weight must be between 20 kg and 400 kg.";

    public const string UserGenderInvalid = "Gender must be either 'male' or 'female'.";

    // Exercise
    public const string ExerciseNotFound = "Exercise not found.";
    public const string ExerciseNameAlreadyExists = "An exercise with the same name already exists.";
    public const string ExerciseCannotDeleteUsedInPastWorkouts =
        "Exercise can't be deleted because it belongs to previous workouts.";
    public const string ExerciseNameRequired = "Name is required.";
    public const string ExerciseNameMaxLength100 = "Name cannot exceed 100 characters.";
    public const string ExerciseMuscleGroupInvalid =
        "MuscleGroup must be one of: back, chest, arms, shoulders, legs, abdomen.";
    public const string ExerciseDifficultyLevelInvalid =
        "DifficultyLevel must be one of: beginner, intermediate, advanced.";

    // Food Item
    public const string FoodItemNotFound = "Food item not found.";
    public const string FoodItemNameRequired = "Name is required.";
    public const string FoodItemNameAlreadyExists = "A food item with the same name already exists.";
    public const string FoodItemCaloriesPositive = "Calories must be greater than 0.";
    public const string FoodItemProteinPositive = "Protein must be greater than 0.";
    public const string FoodItemCarbsPositive = "Carbs must be greater than 0.";
    public const string FoodItemFatPositive = "Fat must be greater than 0.";

    // Food Log
    public const string FoodLogNotFound = "Food log not found.";
    public const string FoodLogBeforeUserRegistration =
        "User can't log food before their registration date.";
    public const string FoodLogServingsPositive = "Servings must be greater than 0.";
    public const string FoodLogDateNotInFuture = "Log date can't be in the future.";
    public const string FoodLogQuantityPositive = "Quantity must be greater than 0.";

    // Goal
    public const string GoalNotFound = "Goal not found.";
    public const string GoalStartDateBeforeUserRegistration =
    "Start date must be greater than or equal to user's registration date.";

    public const string GoalEndDateBeforeStartDate =
        "End date must be greater than or equal to start date.";

    public const string GoalTargetValueInvalidForWeightLoss =
        "Target value must be less than user's current weight for weight loss goals.";

    public const string GoalTargetValueInvalidForWeightGain =
        "Target value must be greater than user's current weight for weight gain goals.";

    public const string GoalOverlapsSameType =
        "User cannot have another goal of the same type that overlaps the selected period.";
    public const string GoalTargetValuePositive = "TargetValue must be a positive number";
    public const string GoalStartDateBeforeOrEqualEndDate =
        "Start date must be less than or equal to end date.";

    // Measurement Log
    public const string MeasurementLogNotFound = "Measurement log not found.";
    public static string MeasurementLogWeightChangeTooLarge(decimal kgPerDay, decimal maxKgPerDay) =>
        $"Weight change is too large: {kgPerDay:0.##} kg/day (max {maxKgPerDay} kg/day).";
    public const string MeasurementLogDateNotInFuture = "Date can't be in the future.";
    public const string MeasurementLogWeightPositive = "Weight must be greater than 0.";
    public const string MeasurementLogBodyFatRange = "BodyFatPercentage must be between 2 and 60%.";
    public const string MeasurementLogWaistRange = "Waist circumference must be within a realistic range.";
    public const string MeasurementLogChestRange = "Chest circumference must be within a realistic range.";
    public const string MeasurementLogArmsRange = "Arm circumference must be within a realistic range.";

    // Workout
    public const string WorkoutNotFound = "Workout not found.";
    public const string WorkoutExerciseAlreadyAdded = "This exercise is already added to the workout.";
    public const string WorkoutDateBeforeUserRegistration =
        "Workout date must be greater than or equal to user's registration date.";

    public const string WorkoutSameStartTimeNotAllowed =
        "User cannot log more than one workout with the same start time.";
    public const string WorkoutDateRequired = "Workout date is required.";
    public const string WorkoutDateNotInFuture = "Workout date cannot be in the future.";
    public const string WorkoutDurationPositive = "Duration must be a positive number.";
    public const string WorkoutDurationRange = "Duration must be between 5 and 300 minutes.";
    public const string WorkoutNotesMaxLength500 = "Notes cannot exceed 500 characters.";

    // Workout Exercise
    public const string WorkoutExerciseNotFound = "Workout exercise not found.";
    public const string WorkoutExerciseSetsPositive = "Sets must be a positive value.";
    public const string WorkoutExerciseRepsPositive = "Reps must be a positive value.";
    public const string WorkoutExerciseWeightUsedNonNegative = "WeightUsed cannot be negative.";
}