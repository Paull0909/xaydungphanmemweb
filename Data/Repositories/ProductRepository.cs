using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
