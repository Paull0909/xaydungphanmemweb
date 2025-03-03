using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class VariantsProductRepository : RepositoryBase<Variants_product, int>, IVariants_productRepository
    {
        private readonly IMapper _mapper;

        public VariantsProductRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

    }
}
