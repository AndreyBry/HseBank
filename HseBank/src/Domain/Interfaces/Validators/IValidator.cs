namespace HseBank.src.Domain.Interfaces.Validators
{
    /// <summary>
    /// Предоставляет методы для выполения валидации объекта.
    /// </summary>
    /// <typeparam name="TRequest">Тип объекта.</typeparam>
    public interface IValidator<TRequest>
    {
        /// <summary>
        /// Валидация объекта.
        /// </summary>
        /// <param name="request">ДТО с данными об объекте.</param>
        public void Validate(TRequest request);
    }
}
