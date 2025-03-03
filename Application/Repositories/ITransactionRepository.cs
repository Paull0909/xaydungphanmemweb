using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, int>
    {
    }
}
