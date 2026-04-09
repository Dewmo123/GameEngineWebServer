namespace BLL.Common.Results
{
    public enum ResultStatus
    {
        Success,
        Created,
        Unauthorized,
        Invalid,
        NotFound,
        Conflict,
        PersistenceFailure
    }

    public class Result
    {
        protected Result(ResultStatus status, string? message)
        {
            Status = status;
            Message = message;
        }

        public ResultStatus Status { get; }
        public string? Message { get; }
        public bool Succeeded => IsSuccessStatus(Status);

        protected static bool IsSuccessStatus(ResultStatus status)
        {
            return status is ResultStatus.Success or ResultStatus.Created;
        }

        public static Result Success(string? message = null)
        {
            return new Result(ResultStatus.Success, message);
        }

        public static Result Created(string? message = null)
        {
            return new Result(ResultStatus.Created, message);
        }

        public static Result Failure(ResultStatus status, string message)
        {
            if (IsSuccessStatus(status))
                throw new ArgumentException("Use a success factory for successful statuses.", nameof(status));

            return new Result(status, message);
        }

        public static Result Unauthorized(string message)
        {
            return Failure(ResultStatus.Unauthorized, message);
        }

        public static Result Invalid(string message)
        {
            return Failure(ResultStatus.Invalid, message);
        }

        public static Result NotFound(string message)
        {
            return Failure(ResultStatus.NotFound, message);
        }

        public static Result Conflict(string message)
        {
            return Failure(ResultStatus.Conflict, message);
        }

        public static Result PersistenceFailure(string message)
        {
            return Failure(ResultStatus.PersistenceFailure, message);
        }
    }

    public sealed class Result<T> : Result
    {
        private Result(ResultStatus status, T? value, string? message) : base(status, message)
        {
            Value = value;
        }

        public T? Value { get; }

        public static Result<T> Success(T value, string? message = null)
        {
            return new Result<T>(ResultStatus.Success, value, message);
        }

        public static Result<T> Created(T value, string? message = null)
        {
            return new Result<T>(ResultStatus.Created, value, message);
        }

        public static new Result<T> Failure(ResultStatus status, string message)
        {
            if (IsSuccessStatus(status))
                throw new ArgumentException("Use a success factory for successful statuses.", nameof(status));

            return new Result<T>(status, default, message);
        }

        public static new Result<T> Unauthorized(string message)
        {
            return Failure(ResultStatus.Unauthorized, message);
        }

        public static new Result<T> Invalid(string message)
        {
            return Failure(ResultStatus.Invalid, message);
        }

        public static new Result<T> NotFound(string message)
        {
            return Failure(ResultStatus.NotFound, message);
        }

        public static new Result<T> Conflict(string message)
        {
            return Failure(ResultStatus.Conflict, message);
        }

        public static new Result<T> PersistenceFailure(string message)
        {
            return Failure(ResultStatus.PersistenceFailure, message);
        }
    }
}
