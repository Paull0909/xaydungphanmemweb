using Application.Repositories;

namespace Application.SeedWorks
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        Task<int> CompleteAsync();
    }
}
