using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        Task<List<Order>> GetAllByUser(Guid id);
    }
}
