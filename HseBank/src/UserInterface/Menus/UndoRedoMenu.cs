using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Menus;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    /// <summary>
    /// Меню для работы с историей команд.
    /// </summary>
    public class UndoRedoMenu : MenuExtensions, IUndoRedoMenu
    {
        private IUndoRedoFacade _undoRedoFacade;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="undoRedoFacade">Фасад истории команд.</param>
        public UndoRedoMenu(IUndoRedoFacade undoRedoFacade)
        {
            _undoRedoFacade = undoRedoFacade;
        }

        /// <summary>
        /// Запуск процесса отмены последнего действия.
        /// </summary>
        public void UndoLastAction()
        {
            var result = _undoRedoFacade.Undo();
            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Действие отменено![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]ℹ️ {result.Message}[/]");
            }
            WaitForKey();
        }

        /// <summary>
        /// Запуск процесса повтора отмененного действия.
        /// </summary>
        public void RedoAction()
        {
            var result = _undoRedoFacade.Redo();
            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Действие повторено![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]ℹ️ {result.Message}[/]");
            }
            WaitForKey();
        }
    }
}
