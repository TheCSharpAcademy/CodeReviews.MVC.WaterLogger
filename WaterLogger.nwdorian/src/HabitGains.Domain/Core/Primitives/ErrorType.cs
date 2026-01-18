namespace HabitGains.Domain.Core.Primitives;

/// <summary>
/// Specifies the category of an <see cref="Error"/>.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Represents a general failure error.
    /// </summary>
    Failure = 0,

    /// <summary>
    /// Indicates that the error is related to validation of input or state.
    /// </summary>
    Validation = 1,

    /// <summary>
    /// Indicates that a requested resource was not found.
    /// </summary>
    NotFound = 2,

    /// <summary>
    /// Indicates that a conflict occurred, such as attempting to create
    /// a resource that already exists or performing an operation that
    /// violates business rules.
    /// </summary>
    Conflict = 3,
}
