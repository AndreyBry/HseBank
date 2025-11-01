namespace HseBank.src.Domain.Models.Results
{
    public class OperationResult
    {
        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }
        public string Message { get; }

        public static OperationResult Success(string? message) => new OperationResult(true, message ?? "Операция выполнена успешно.");
        public static OperationResult Failure(string message) => new OperationResult(false, message);
    }
}
