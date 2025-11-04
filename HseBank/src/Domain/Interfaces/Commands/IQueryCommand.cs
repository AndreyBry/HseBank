namespace HseBank.src.Domain.Interfaces.Commands
{
    /// <summary>
    /// Определяет контракт для команды-запроса, требующей возврата результата.
    /// </summary>
    /// <typeparam name="T">Тип данных результата.</typeparam>
    public interface IQueryCommand<T>
    {
        /// <summary>
        /// Выполнение команды.
        /// </summary>
        /// <returns>Результат выполнения команды-запроса.</returns>
        T Execute();
    }
}
