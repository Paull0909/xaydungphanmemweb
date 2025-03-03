using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IProductImageRepository : IRepository<ProductImage, int>
    {
        Task<List<ProductImage>> GetListImgByIdProAsync(int id);
    }
}
