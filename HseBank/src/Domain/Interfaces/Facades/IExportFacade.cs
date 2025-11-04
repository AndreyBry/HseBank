using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    /// <summary>
    /// Предоставляет методы для экспорта данных в файл.
    /// </summary>
    public interface IExportFacade
    {
        /// <summary>
        /// Экспорт данных в файл.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Export(string filePath, FileFormat fileFormat);
    }
}
