namespace HabitGains.Domain.Core.Primitives;

/// <summary>
/// Represents a concrete domain error.
/// </summary>
public record Error
{
    /// <summary>
    /// Gets the empty error instance.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">A human-readable description of the error.</param>
    /// <param name="type">The <see cref="ErrorType"/> indicating the category of the error.</param>
    protected Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the error description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the error type.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    /// Creates a failure error with the specified code and description.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    /// <returns>A new <see cref="Error"/> instance with <see cref="ErrorType.Failure"/></returns>
    public static Error Failure(string code, string description)
    {
        return new(code, description, ErrorType.Failure);
    }

    /// <summary>
    /// Creates a validation error with the specified code and description.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    /// <returns>A new <see cref="Error"/> instance with <see cref="ErrorType.Validation"/></returns>
    public static Error Validation(string code, string description)
    {
        return new(code, description, ErrorType.Validation);
    }

    /// <summary>
    /// Creates a not-found error with the specified code and description.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    /// <returns>A new <see cref="Error"/> instance with <see cref="ErrorType.NotFound"/></returns>
    public static Error NotFound(string code, string description)
    {
        return new(code, description, ErrorType.NotFound);
    }

    /// <summary>
    /// Creates a conflict error with the specified code and description.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    /// <returns>A new <see cref="Error"/> instance with <see cref="ErrorType.Conflict"/></returns>
    public static Error Conflict(string code, string description)
    {
        return new(code, description, ErrorType.Conflict);
    }
}
