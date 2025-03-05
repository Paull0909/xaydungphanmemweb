using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail, int>, IOrderDetailRepository
    {
        private readonly IMapper _mapper;

        public OrderDetailRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<OrderDetail>> GetAllByBill(int id)
        {
            var ord = _context.OrderDetails.Where(x => x.bill_id == id).ToList();
            return ord;
        }
    }
}
