namespace BLL.Common.Results
{
    public enum ErrorCode
    {
        Unauthorized,
        InvalidInput,
        NotFound,
        Conflict,
        PersistenceFailure
    }

    public sealed record Error(ErrorCode Code, string Message);

    public class Result
    {
        protected Result(bool succeeded, Error? error)
        {
            Succeeded = succeeded;
            Error = error;
        }

        public bool Succeeded { get; }
        public Error? Error { get; }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(ErrorCode code, string message)
        {
            return new Result(false, new Error(code, message));
        }

        public static Result Unauthorized(string message)
        {
            return Failure(ErrorCode.Unauthorized, message);
        }

        public static Result Invalid(string message)
        {
            return Failure(ErrorCode.InvalidInput, message);
        }

        public static Result NotFound(string message)
        {
            return Failure(ErrorCode.NotFound, message);
        }

        public static Result Conflict(string message)
        {
            return Failure(ErrorCode.Conflict, message);
        }

        public static Result PersistenceFailure(string message)
        {
            return Failure(ErrorCode.PersistenceFailure, message);
        }
    }

    public sealed class Result<T> : Result
    {
        private Result(T value) : base(true, null)
        {
            Value = value;
        }

        private Result(Error error) : base(false, error)
        {
        }

        public T? Value { get; }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static new Result<T> Failure(ErrorCode code, string message)
        {
            return new Result<T>(new Error(code, message));
        }

        public static new Result<T> Unauthorized(string message)
        {
            return Failure(ErrorCode.Unauthorized, message);
        }

        public static new Result<T> Invalid(string message)
        {
            return Failure(ErrorCode.InvalidInput, message);
        }

        public static new Result<T> NotFound(string message)
        {
            return Failure(ErrorCode.NotFound, message);
        }

        public static new Result<T> Conflict(string message)
        {
            return Failure(ErrorCode.Conflict, message);
        }

        public static new Result<T> PersistenceFailure(string message)
        {
            return Failure(ErrorCode.PersistenceFailure, message);
        }
    }
}
