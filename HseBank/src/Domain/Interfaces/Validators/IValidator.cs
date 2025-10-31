namespace HseBank.src.Domain.Interfaces.Validators
{
    public interface IValidator<TRequest>
    {
        public void Validate(TRequest request);
    }
}
