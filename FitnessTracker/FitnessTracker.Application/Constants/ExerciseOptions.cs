namespace FitnessTracker.Application.Constants;

public static class ExerciseOptions
{
    public static readonly HashSet<string> MuscleGroups =
        new(new[] { "back", "chest", "arms", "shoulders", "legs", "abdomen" },
            StringComparer.OrdinalIgnoreCase);

    public static readonly HashSet<string> DifficultyLevels =
        new(new[] { "beginner", "intermediate", "advanced" },
            StringComparer.OrdinalIgnoreCase);
}
