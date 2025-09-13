namespace AI_service.Shared.Outcome;

internal class Result
{
    internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }

    public Error Error { get; }

    internal static Result Success()
        => new(true, Error.None);

    internal static Result<TValue> Success<TValue>(TValue value)
        => new(value, true, Error.None);

    internal static Result Failure(Error error)
        => new(false, error);

    internal static Result<TValue> Failure<TValue>(Error error)
        => new(default!, false, error);
}

internal class Result<TValue> : Result
{
    private readonly TValue? _value;

    internal TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Can't access Value of a failure.");

    internal Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error) => _value = value;

    internal static Result<TValue> ValidationFailure(Error error)
        => new(default!, false, error);

    public static implicit operator Result<TValue>(TValue? value)
        => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}
