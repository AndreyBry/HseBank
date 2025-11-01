using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Interfaces.Menus
{
    public interface IOperationMenu : IMenu
    {
        public void AddOperation(TransactionType type);
    }
}
