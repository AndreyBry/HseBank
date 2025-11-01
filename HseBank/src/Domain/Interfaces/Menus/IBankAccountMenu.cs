namespace HseBank.src.Domain.Interfaces.Menus
{
    public interface IBankAccountMenu : IMenu
    {
        public void CreateBankAccount();
        public void ChangeBankAccountName();
        public void DeleteBankAccount();
        public void ShowBankAccounts();
    }
}
