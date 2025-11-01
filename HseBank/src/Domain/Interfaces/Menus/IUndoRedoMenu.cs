namespace HseBank.src.Domain.Interfaces.Menus
{
    public interface IUndoRedoMenu
    {
        public void UndoLastAction();
        public void RedoAction();
    }
}
