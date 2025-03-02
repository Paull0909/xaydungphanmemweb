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

    }
}
