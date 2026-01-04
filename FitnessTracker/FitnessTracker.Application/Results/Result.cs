namespace FitnessTracker.Application.Results;

public class Result
{
    public bool Success { get; }
    public string? Error { get; }

    protected Result(bool success, string? error)
    {
        Success = success;
        Error = error;
    }

    public static Result Ok()
        => new Result(true, null);

    public static Result Fail(string error)
        => new Result(false, error);
}

public class Result<T> : Result
{
    public T? Data { get; }

    protected Result(bool success, T? data, string? error)
        : base(success, error)
    {
        Data = data;
    }

    public static Result<T> Ok(T data)
        => new Result<T>(true, data, null);

    public static new Result<T> Fail(string error)
        => new Result<T>(false, default, error);
}
