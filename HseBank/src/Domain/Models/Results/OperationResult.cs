namespace HseBank.src.Domain.Models.Results
{
    public class OperationResult
    {
        private bool _isSuccess;
        private string _message;

        public bool IsSuccess => _isSuccess;
        public string Message => _message;

        protected OperationResult(bool isSuccess, string message)
        {
            _isSuccess = isSuccess;
            _message = message;
        }

        public static OperationResult Success(string? message) => new OperationResult(true, message ?? "Операция выполнена успешно.");
        public static OperationResult Failure(string message) => new OperationResult(false, message);
    }
}
