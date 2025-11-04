namespace HseBank.src.Domain.Exceptions
{
    /// <summary>
    /// Исключение, описывающее ошибку в бизнес-процессе.
    /// </summary>
    public class BusinessRuleException : Exception
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public BusinessRuleException(string message) : base(message) { }
    }
}
