namespace HabitGains.Domain.Core.Primitives;

/// <summary>
/// Represents the result of some operation, with status information and possibly a value and an error.
/// </summary>
/// <typeparam name="TValue">The result value type.</typeparam>
/// <param name="value">The result value.</param>
/// <param name="isSuccess">The flag indicating if the result is successful.</param>
/// <param name="error">The error.</param>
public class Result<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
    /// <summary>
    /// Gets the result value if the result is successful, otherwise throws an exception.
    /// </summary>
    /// <returns>The result value if the result is successful.</returns>
    /// <exception cref="InvalidOperationException"> when <see cref="Result.IsFailure"/> is true.</exception>
    public TValue Value =>
        IsSuccess ? value! : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="TValue"/>
    /// into a successful <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to wrap in a success result.</param>
    /// <returns>A successful <see cref="Result{TValue}"/> containing the specified value.</returns>
    public static implicit operator Result<TValue>(TValue value)
    {
        return Success(value);
    }

    /// <summary>
    /// Implicitly converts an <see cref="Error"/>
    /// into a failed <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="error">The error to wrap in a failure result.</param>
    /// <returns>
    /// A failed <see cref="Result{TValue}"/> containing the specified error.
    /// </returns>
    public static implicit operator Result<TValue>(Error error)
    {
        return Failure<TValue>(error);
    }
}
