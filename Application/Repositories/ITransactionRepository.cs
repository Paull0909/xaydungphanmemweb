using Application.DTO;
using Application.DTO.Transactions;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, int>
    {
        Task<PagedResult<TransactionDTO>> GetTransactionPagingAsync(PagedRequest request);
    }
}
