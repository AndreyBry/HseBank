namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Предоставляет методы для выбора пользователем действий, связанных с банковскими счетами, и ввода необходимых данных.
    /// </summary>
    public interface IBankAccountMenu : IMenu
    {
        /// <summary>
        /// Создание счета.
        /// </summary>
        public void CreateBankAccount();

        /// <summary>
        /// Изменение название счета.
        /// </summary>
        public void ChangeBankAccountName();

        /// <summary>
        /// Удаление счета.
        /// </summary>
        public void DeleteBankAccount();

        /// <summary>
        /// Отображение всех счетов.
        /// </summary>
        public void ShowBankAccounts();
    }
}
