using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail, int>
    {
        Task<List<OrderDetail>> GetAllByBill(int id);
    }
}
