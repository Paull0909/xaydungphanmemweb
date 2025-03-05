using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TotalRevenueRepository : RepositoryBase<TotalRevenue, int>, ITotalRevenueRepository
    {
        private readonly IMapper _mapper;

        public TotalRevenueRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<TotalRevenue>> GetAll()
        {
            var list = await _context.TotalRevenue.Select(p => new TotalRevenue()
            {
                doanhthu_id =p.doanhthu_id,
                date = p.date,
                tongdoanhthu = p.tongdoanhthu,
                numberofsales = p.numberofsales,

            }).OrderByDescending(p => p.date.Year).ToListAsync();
            return list;

        }

        public async Task<List<TotalRevenue>> GetAllforYear(int year)
        {
            var total = await _context.TotalRevenue.Where(t=>t.date.Year == year).ToListAsync();
            return total;
        }

        public async Task<TotalRevenue> GetByDate(DateTime date)
        {
            var total = _context.TotalRevenue.Where(x => x.date.Year == date.Year & x.date.Month == date.Month).FirstOrDefault();
            return total;
        }
    }
}
