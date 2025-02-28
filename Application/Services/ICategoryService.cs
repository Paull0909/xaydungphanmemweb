using Application.DTO.Categorys;
using Application.Entities;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task<int> CreateCategoryAsync(Category category);
        Task<int> UpdateCategoryAsync(int id, Category category);
        Task<int> DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    }
}
