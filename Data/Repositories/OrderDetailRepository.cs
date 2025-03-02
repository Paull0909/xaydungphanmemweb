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

    }
}
