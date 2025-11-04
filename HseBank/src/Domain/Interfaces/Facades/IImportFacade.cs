using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    /// <summary>
    /// Предоставляет методы для импорта данных из файла.
    /// </summary>
    public interface IImportFacade
    {
        /// <summary>
        /// Импорт данных из файла.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Import(string filePath, FileFormat fileFormat);
    }
}
