using Application.DTO;
using Application.DTO.Orders;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        Task<PagedResult<OrderDTO>> GetOrderPagingAsync(PagedRequest request);
        Task<List<Order>> GetAllByUser(Guid id);
    }
}
