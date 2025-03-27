using Application.DTO;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IVariants_productRepository : IRepository<Variants_product, int>
    {
        Task<PagedResult<VariantsProductDTO>> GetVariantsProductPagingAsync(PagedRequest request);
        Task<List<Variants_product>> GetByProduct(int id);
        Task<Variants_product> Loadwhenbuyer(int id,string name);
    }
}
