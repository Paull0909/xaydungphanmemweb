using Application.DTO.TotalRevenues;
using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class TotalRevenueRepository : RepositoryBase<TotalRevenue, int>, ITotalRevenueRepository
    {
        private readonly IMapper _mapper;

        public TotalRevenueRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<TotalRevenueDTO>> GetDetailforYear(int year)
        {

            var list = await _context.TotalRevenue.Select(p => new TotalRevenueDTO()
            {
                doanhthu_id =p.doanhthu_id,
                date = p.date,
                tongdoanhthu = p.tongdoanhthu,
                numberofsales = p.numberofsales,

            }).Where(p => p.date.Year == year).ToListAsync();
            return list;

        }

        public async Task<List<TotalRevenuesGetYear>> GetAllforYear()
        {
            var list = await _context.TotalRevenue.Select(p=>new TotalRevenueDTO
            {
                doanhthu_id = p.doanhthu_id,
                date = p.date,
                tongdoanhthu = p.tongdoanhthu,
                numberofsales = p.numberofsales,
            }).OrderByDescending(p => p.date.Year).ToListAsync();
            var group = list.GroupBy(x => x.date.Year).Select(group => new TotalRevenuesGetYear
            {
                Date = group.Key,
                NumberofSales = group.Sum(p => p.numberofsales),
                Totalprice = group.Sum(p => p.tongdoanhthu)
            }).ToList();
            return group;
        }

        public async Task<int> AddWhenbyOder(TotalRevenue request)
        {
            var time = await _context.TotalRevenue.FirstOrDefaultAsync(t => t.date.Year == request.date.Year && t.date.Month == request.date.Month);
            if (time != null)
            {
                time.tongdoanhthu += request.tongdoanhthu;
                time.numberofsales += 1;
                _context.Update(time);

                await _context.SaveChangesAsync();
                return time.doanhthu_id;
            }
            else
            {
                _context.TotalRevenue.Add(request);
                await _context.SaveChangesAsync();
                return request.doanhthu_id;
            }
        }
    }
}
