﻿using Application.DTO;
using Application.DTO.Categorys;
using Application.DTO.Orders;
using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order, int>, IOrderRepository
    {
        private readonly IMapper _mapper;

        public OrderRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<Order>> GetAllByUser(Guid id)
        {
            var or = _context.Orders.Where(x => x.UserId == id).ToList();
            return or;
        }

        public async Task<PagedResult<OrderDTO>> GetOrderPagingAsync(PagedRequest request)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(x => x.ShipName.Contains(request.search));
            }
            //Query page
            query = query
                .OrderByDescending(x => x.bill_id)
                .Skip((request.page - 1) * request.size)
                .Take(request.size);
            var listOrder = query.ToListAsync();
            var rowCount = listOrder.Result.Count();
            List<OrderDTO> orderDtos = new List<OrderDTO>();
            listOrder.Result.ForEach((x) =>
            {
                orderDtos.Add(new OrderDTO
                {
                    bill_id = x.bill_id,
                    ShipName = x.ShipName,
                    Note = x.Note,
                    OrderDate = x.OrderDate,
                    ShipAddress = x.ShipAddress,
                    ShipEmail = x.ShipEmail,
                    ShipPhoneNumber = x.ShipPhoneNumber,
                    ShippingFee = x.ShippingFee,
                    Status = x.Status,
                    Totalprice = x.Totalprice,
                    transactionStatus = x.transactionStatus,
                    UserId = x.UserId
                });
            });

            var pagedResponse = new PagedResult<OrderDTO>
            {
                Data = orderDtos,
                CurrentPage = request.page,
                RowCount = rowCount,
                PageSize = request.size,
            };
            return pagedResponse;
        }
    }
}
