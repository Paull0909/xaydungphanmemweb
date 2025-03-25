using Application.DTO.TotalRevenues;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ITotalRevenueRepository : IRepository<TotalRevenue, int>
    {
        Task<List<TotalRevenueDTO>> GetDetailforYear(int year);
        Task<List<TotalRevenuesGetYear>> GetAllforYear();
        Task<int> AddWhenbyOder(TotalRevenue request);
    }
}
