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
            Products= new ProductRepository(context, mapper);
            AdventisementRepository= new AdventisementRepository(context,mapper);
            CartRepository = new CartRepostory(context,mapper);
            OrderDetailRepository = new OrderDetailRepository(context, mapper);
            OrderRepository = new OrderRepository(context, mapper);
            ProductImageRepository = new ProductImageRepository(context, mapper);
            SizeProductsRepository = new SizeProductRepository(context, mapper);
            TotalRevenueRepository = new TotalRevenueRepository(context, mapper);
            TransactionRepository = new TransactionRepository(context, mapper);
            VariantsProductRepository = new VariantsProductRepository(context, mapper);
        }
        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IAdventisementRepository AdventisementRepository { get; private set; }
        public ICartRepository CartRepository { get; private set; }
        public IOrderDetailRepository OrderDetailRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IProductImageRepository ProductImageRepository { get; private set; }
        public ISizeProductRepository SizeProductsRepository { get; private set; }
        public ITotalRevenueRepository TotalRevenueRepository { get; private set; }
        public ITransactionRepository TransactionRepository { get; private set; }
        public IVariants_productRepository VariantsProductRepository { get; private set; }

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