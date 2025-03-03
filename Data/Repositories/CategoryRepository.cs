using Application.DTO;
using Application.DTO.Categorys;
using Application.Entities;
using Application.Repositories;
using Application.SeedWorks;
using AutoMapper;
using Azure;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
    {
        private readonly IMapper _mapper;

        public CategoryRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<int> CountCategoryNormalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<CategoryDTO>> GetCategoryPagingAsync(PagedRequest request)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(x => x.type_name.Contains(request.search));
            }
            //Query page
            query = query
                .OrderByDescending(x => x.type_id)
                .Skip((request.page - 1) *request.size)
                .Take(request.size);
            var listCategory= query.ToListAsync();
            var rowCount = listCategory.Result.Count();
            List<CategoryDTO> categoryDtos = new List<CategoryDTO>();
            listCategory.Result.ForEach((x) =>
            {
                categoryDtos.Add(new CategoryDTO
                {
                   type_id = x.type_id,
                   type_name=x.type_name,
                });
            });

            var pagedResponse = new PagedResult<CategoryDTO>
            {
                Data = categoryDtos,
                CurrentPage = request.page,
                RowCount = rowCount,
                PageSize = request.size,
            };
            return pagedResponse;
        }
    }
}
