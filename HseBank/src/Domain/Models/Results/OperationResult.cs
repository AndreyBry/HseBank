namespace HseBank.src.Domain.Models.Results
{
    /// <summary>
    /// Результат выполения операции.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="isSuccess">Успешность выполнения.</param>
        /// <param name="message">Сообщение.</param>
        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }
        public string Message { get; }

        /// <summary>
        /// Создание результата успешного выполения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Результат успешной операции.</returns>
        public static OperationResult Success(string? message) => new OperationResult(true, message ?? "Операция выполнена успешно.");

        /// <summary>
        /// Создание результата выполнения с исключением.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Результат операции с исключением.</returns>
        public static OperationResult Failure(string message) => new OperationResult(false, message);
    }
}
