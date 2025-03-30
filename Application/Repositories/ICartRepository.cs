using Application.DTO.Carts;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ICartRepository : IRepository<Cart, int>
    {
        Task<List<CartDTO>> GetAllByUser(Guid id);
    }
}
