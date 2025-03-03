using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IVariants_productRepository : IRepository<Variants_product, int>
    {
        Task<List<Variants_product>> GetByProduct(int id);
    }
}
