namespace HseBank.src.Domain.Interfaces.Menus
{
    public interface ICategoryMenu : IMenu
    {
        public void CreateCategory();
        public void ChangeCategoryName();
        public void DeleteCategory();
        public void ShowCategories();
    }
}
