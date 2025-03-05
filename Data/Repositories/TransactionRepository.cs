using Application.DTO;
using Application.DTO.Products;
using Application.DTO.Transactions;
using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    internal class TransactionRepository : RepositoryBase<Transaction, int>, ITransactionRepository
    {
        private readonly IMapper _mapper;

        public TransactionRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<TransactionDTO>> GetTransactionPagingAsync(PagedRequest request)
        {
            var query = _context.Transaction.AsQueryable();
            if (!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(x => x.Message.Contains(request.search));
            }
            //Query page
            query = query
                .OrderByDescending(x => x.Id)
                .Skip((request.page - 1) * request.size)
                .Take(request.size);
            var list = query.ToListAsync();
            var rowCount = list.Result.Count();
            List<TransactionDTO> transactionDtos = new List<TransactionDTO>();
            list.Result.ForEach((x) =>
            {
                transactionDtos.Add(new TransactionDTO
                {
                    Id = x.Id,
                    Message = x.Message,
                    Amount = x.Amount,
                    ExternalTransactionId = x.ExternalTransactionId,
                    Fee = x.Fee,
                    Provider = x.Provider,
                    Result = x.Result,
                    Status = x.Status,
                    TransactionDate = x.TransactionDate,
                    UserId = x.UserId
                });
            });

            var pagedResponse = new PagedResult<TransactionDTO>
            {
                Data = transactionDtos,
                CurrentPage = request.page,
                RowCount = rowCount,
                PageSize = request.size,
            };
            return pagedResponse;
        }
    }
}
