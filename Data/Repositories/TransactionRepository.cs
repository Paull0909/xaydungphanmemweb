using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;

namespace Data.Repositories
{
    internal class TransactionRepository : RepositoryBase<Transaction, int>, ITransactionRepository
    {
        private readonly IMapper _mapper;

        public TransactionRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

    }
}
