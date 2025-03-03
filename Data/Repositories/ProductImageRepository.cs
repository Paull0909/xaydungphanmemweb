using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class ProductImageRepository : RepositoryBase<ProductImage, int>, IProductImageRepository
    {
        private readonly IMapper _mapper;
        

        public ProductImageRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ProductImage> GetImgByIdProductAsync(int id)
        {
            var img = _context.ProductImages.Find(id);
            return img;
        }

        public async Task<List<ProductImage>> GetListImgByIdProAsync(int id)
        {
            var img = _context.ProductImages.Where(t=>t.product_id == id).ToList();
            return img;
        }
    }
}
