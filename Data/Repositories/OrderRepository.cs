using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order, int>, IOrderRepository
    {
        private readonly IMapper _mapper;

        public OrderRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<Order>> GetAllByUser(Guid id)
        {
            var or = _context.Orders.Where(x => x.UserId == id).ToList();
            return or;
        }
    }
}
