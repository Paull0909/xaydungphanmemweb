using Application.DTO;
using Application.DTO.Categorys;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<int> CountCategoryNormalAsync();
        Task<PagedResult<CategoryDTO>> GetCategoryPagingAsync(PagedRequest request);
    }
}
