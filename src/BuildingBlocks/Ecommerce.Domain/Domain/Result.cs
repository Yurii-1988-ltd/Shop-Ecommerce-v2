namespace Ecommerce.Domain.Domain;

public class Result
{
    public bool IsSuccess { get;  }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new ArgumentException(
                "Success result cannot have an error.",
                nameof(error));

        if (!isSuccess && error == Error.None)
            throw new ArgumentException(
                "Failure result must have an error.",
                nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result Success()=> new (true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T value)
    {
        return Result<T>.Success(value);
    }
    public static Result<T> Failure<T>(Error error)
    {
        return Result<T>.Failure(error);
    }

    public static implicit operator Result(Error error)=>Failure(error);

    public TResult Match<TResult>(Func<TResult>onSuccess,
                                    Func<Error,TResult>onFailure)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);
        return IsSuccess ? onSuccess()
            : onFailure(Error);

    }
}


public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access value of a failed result");

    private Result(TValue value) : base(true, Error.None)
    {
        _value = value;
    }

    private Result(Error error) : base(false, error)
    {
        _value = default;
    }
    public TResult Match<TResult>(
    Func<TValue, TResult> onSuccess,
    Func<Error, TResult> onFailure)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return IsSuccess
            ? onSuccess(Value)
            : onFailure(Error);
    }
    public static Result<TValue> Success(TValue value) => new(value);
    public new static Result<TValue> Failure(Error error) => new(error);

    // Implicit conversions for cleaner syntax
    public static implicit operator Result<TValue>(TValue value) => Success(value);
    public static implicit operator Result<TValue>(Error error) => Failure(error);
}