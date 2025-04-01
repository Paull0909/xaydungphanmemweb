using Application.DTO.Carts;
using Application.DTO.Products;
using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CartRepostory : RepositoryBase<Cart, int>, ICartRepository
    {
        private readonly IMapper _mapper;

        public CartRepostory(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<CartDTO>> GetAllByUser(Guid id)
        {
            var cart = await  _context.Carts.Where(c=>c.UserId == id).ToListAsync();
            var dto = _mapper.Map<List<CartDTO>>(cart);
            foreach (var item in dto)
            {
                var i = _context.Products.Find(item.product_id);
                item.Products = _mapper.Map<ProductDTO>(i);
                item.ProductImages = _context.ProductImages.FirstOrDefault(p => p.product_id == item.product_id);
            }
            return dto;
        }
    }
}
