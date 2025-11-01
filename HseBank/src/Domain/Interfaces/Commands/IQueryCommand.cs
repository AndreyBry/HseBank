namespace HseBank.src.Domain.Interfaces.Commands
{
    public interface IQueryCommand<T>
    {
        T Execute();
    }
}
