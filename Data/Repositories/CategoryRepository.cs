using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

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

        public Task<List<Category>> GetCategoryNormalsAsync(string? key, int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
