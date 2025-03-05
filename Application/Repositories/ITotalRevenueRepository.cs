using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ITotalRevenueRepository : IRepository<TotalRevenue, int>
    {
        Task<TotalRevenue> GetByDate(DateTime date);
        Task<List<TotalRevenue>> GetAll();
        Task<List<TotalRevenue>> GetAllforYear(int year);
    }
}
