using Application.Repositories;

namespace Application.SeedWorks
{
    public interface IUnitOfWork
    {
        IAdventisementRepository AdventisementRepository { get; }
        ICategoryRepository Categories { get; }
        ICartRepository CartRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductRepository Products { get; }
        ISizeProductRepository SizeProductsRepository { get; }
        ITotalRevenueRepository TotalRevenueRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IVariants_productRepository VariantsProductRepository { get; }
        Task<int> CompleteAsync();
    }
}
