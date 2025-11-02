namespace HseBank.src.Domain.Interfaces.Menus
{
    public interface IMetricsMenu : IMenu
    {
        public void ShowCommandMetrics();
        public void ClearMetrics();
    }
}
