namespace AI_service.Shared.Outcome
{
    internal record Error
    {
        internal static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        internal static readonly Error NullValue = new(
            "General.Null",
            "Null value was provided",
            ErrorType.Failure);

        internal Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        internal string Code { get; }

        internal string Description { get; }

        internal ErrorType Type { get; }

        internal static Error Failure(string code, string description) =>
            new(code, description, ErrorType.Failure);

        internal static Error NotFound(string code, string description) =>
            new(code, description, ErrorType.NotFound);

        internal static Error Problem(string code, string description) =>
            new(code, description, ErrorType.Problem);

        internal static Error Conflict(string code, string description) =>
            new(code, description, ErrorType.Conflict);
    }

    internal enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        Problem = 2,
        NotFound = 3,
        Conflict = 4
    }
}
