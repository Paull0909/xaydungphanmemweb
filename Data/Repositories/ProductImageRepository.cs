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

    }
}
