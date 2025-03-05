using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class SizeProductRepository : RepositoryBase<Size_Product, int>, ISizeProductRepository
    {
        private readonly IMapper _mapper;

        public SizeProductRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<Size_Product>> GetByProduct(int id)
        {
            var size = _context.Size_Product.Where(x => x.variants_product_id==id).ToList();
            return size;
        }
    }
}
