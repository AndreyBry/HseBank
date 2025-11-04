namespace HseBank.src.Domain.Models.Results
{
    /// <summary>
    /// Результат выполения операции с результатом выполнения.
    /// </summary>
    /// <typeparam name="T">Тип данных возвращаемого результата.</typeparam>
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data">Результат выполнения.</param>
        /// <param name="isSuccess">Успешность выполнения.</param>
        /// <param name="message">Сообщение.</param>
        public OperationResult(T data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }

        public T Data { get; }

        /// <summary>
        /// Создание результата успешного выполнения.
        /// </summary>
        /// <param name="data">Результат выполнения.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns>Результат успешной операции.</returns>
        public static OperationResult<T> Success(T data, string? message) => new OperationResult<T>(data, true, message ?? "Операция выполнена успешно.");

        /// <summary>
        /// Создание результата выполнения с исключением.
        /// </summary>
        /// <param name="data">Результат выполнения.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns>Результат операции с исключением.</returns>
        public static OperationResult<T> Failure(T data, string message) => new OperationResult<T>(data, false, message);
    }
}
