using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    public class CartRepostory : RepositoryBase<Cart, int>, ICartRepository
    {
        private readonly IMapper _mapper;

        public CartRepostory(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<Cart>> GetAllByUser(Guid id)
        {
            var cart = _context.Carts.Where(c=>c.UserId == id).ToList();
            return cart;
        }
    }
}
