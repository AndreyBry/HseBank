using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Menus;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    public class UndoRedoMenu : MenuExtensions, IUndoRedoMenu
    {
        private IUndoRedoFacade _undoRedoFacade;

        public UndoRedoMenu(IUndoRedoFacade undoRedoFacade)
        {
            _undoRedoFacade = undoRedoFacade;
        }

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
