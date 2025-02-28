using Application.Repositories;
using Application.SeedWorks;
using AutoMapper;
using Data.EF;
using Data.Repositories;

namespace Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebDbContext _context;

        public UnitOfWork(WebDbContext context, IMapper mapper)
        {
            _context = context;
            Categories = new CategoryRepository(context, mapper);
        }

        public ICategoryRepository Categories { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}