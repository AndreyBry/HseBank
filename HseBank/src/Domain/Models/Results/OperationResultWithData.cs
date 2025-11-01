namespace HseBank.src.Domain.Models.Results
{
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(T data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }

        public T Data { get; }

        public static OperationResult<T> Success(T data, string? message) => new OperationResult<T>(data, true, message ?? "Операция выполнена успешно.");
        public static OperationResult<T> Failure(T data, string message) => new OperationResult<T>(data, false, message);
    }
}
