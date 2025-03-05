using Application.DTO;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class VariantsProductRepository : RepositoryBase<Variants_product, int>, IVariants_productRepository
    {
        private readonly IMapper _mapper;

        public VariantsProductRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<Variants_product>> GetByProduct(int id)
        {
            var variant = _context.Variants_product.Where(x => x.product_id == id).ToList();
            return variant;
        }

        public async Task<PagedResult<VariantsProductDTO>> GetVariantsProductPagingAsync(PagedRequest request)
        {
            var query = _context.Variants_product.AsQueryable();
            if (!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(x => x.Name.Contains(request.search));
            }
            //Query page
            query = query
                .OrderByDescending(x => x.Id)
                .Skip((request.page - 1) * request.size)
                .Take(request.size);
            var list = query.ToListAsync();
            var rowCount = list.Result.Count();
            List<VariantsProductDTO> variantsProductDtos = new List<VariantsProductDTO>();
            list.Result.ForEach((x) =>
            {
                variantsProductDtos.Add(new VariantsProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    product_id=x.product_id
                });
            });

            var pagedResponse = new PagedResult<VariantsProductDTO>
            {
                Data = variantsProductDtos,
                CurrentPage = request.page,
                RowCount = rowCount,
                PageSize = request.size,
            };
            return pagedResponse;
        }
    }
}
