using Application.DTO;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IVariants_productRepository : IRepository<Variants_product, int>
    {
        Task<PagedResult<VariantsProductDTO>> GetVariantsProductPagingAsync(PagedRequest request);
    }
}
