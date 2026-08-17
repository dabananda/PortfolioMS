using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortfolioMS.Server.Application.Common
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Errors.None)
                throw new ArgumentException();

            if (!isSuccess && error == Errors.None)
                throw new ArgumentException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static Result Success() => new(true, Errors.None);
        public static Result Failure(Error error) => new(false, error);
    }

    public sealed class Result<T> : Result
    {
        private readonly T? _value;

        private Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value => IsSuccess ? _value! : throw new InvalidOperationException("No value exists for failure result.");
        public static Result<T> Success(T value) => new(value, true, Errors.None);
        public new static Result<T> Failure(Error error) => new(default!, false, error);
    }
}
