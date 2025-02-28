using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<List<Category>> GetCategoryNormalsAsync(string? key, int page, int size);
        Task<int> CountCategoryNormalAsync();
    }
}
