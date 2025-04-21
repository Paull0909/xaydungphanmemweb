using Application.DTO;
using Application.DTO.Products;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<PagedResult<ProductDTO>> GetProductPagingAsync(PagedRequest request);
        Task<List<Product>> GetProductofCate(int type_id);
    }
}
