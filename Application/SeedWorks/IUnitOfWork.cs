using Application.Repositories;

namespace Application.SeedWorks
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        Task<int> CompleteAsync();
    }
}
