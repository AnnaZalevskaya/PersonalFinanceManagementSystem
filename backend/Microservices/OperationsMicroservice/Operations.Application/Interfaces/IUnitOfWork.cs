namespace Operations.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ICategoryTypeRepository CategoryTypes { get; }
        IOperationRepository Operations { get; }
    }
}
