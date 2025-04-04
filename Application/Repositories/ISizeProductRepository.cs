﻿using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface ISizeProductRepository : IRepository<Size_Product, int>
    {
        Task<List<Size_Product>> GetByProduct(int id);
        Task<bool> Loadwhenbuyer(int id, string name, int soluong);
    }
}
