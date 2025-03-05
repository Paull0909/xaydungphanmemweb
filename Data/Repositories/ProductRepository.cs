using Application.DTO;
using Application.DTO.Orders;
using Application.DTO.Products;
using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDTO>> GetProductPagingAsync(PagedRequest request)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(x => x.product_name.Contains(request.search));
            }
            //Query page
            query = query
                .OrderByDescending(x => x.product_id)
                .Skip((request.page - 1) * request.size)
                .Take(request.size);
            var listProduct = query.ToListAsync();
            var rowCount = listProduct.Result.Count();
            List<ProductDTO> productDtos = new List<ProductDTO>();
            listProduct.Result.ForEach((x) =>
            {
                productDtos.Add(new ProductDTO
                {
                    product_id = x.product_id,
                    product_name = x.product_name,
                    price = x.price,
                    advertisement_id = x.advertisement_id,
                    Desdescription =x.Desdescription,
                    discount = x.discount,  
                    IsFeatured = x.IsFeatured,
                    status = x.status,
                    type_id = x.type_id 
                });
            });

            var pagedResponse = new PagedResult<ProductDTO>
            {
                Data = productDtos,
                CurrentPage = request.page,
                RowCount = rowCount,
                PageSize = request.size,
            };
            return pagedResponse;
        }
    }
}
