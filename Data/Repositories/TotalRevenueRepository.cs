using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class TotalRevenueRepository : RepositoryBase<TotalRevenue, int>, ITotalRevenueRepository
    {
        private readonly IMapper _mapper;

        public TotalRevenueRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<TotalRevenue> GetByDate(DateTime date)
        {
           var total = _context.TotalRevenue.Where(x=>x.date.Year == date.Year & x.date.Month == date.Month).FirstOrDefault();
            return total;
        }
    }
}
