using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class AdventisementRepository : RepositoryBase<Advertisement, int>, IAdventisementRepository
    {
        private readonly IMapper _mapper;

        public AdventisementRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

    }
}
