﻿using Application.DTO.Products;
using Application.Entities;
using AutoMapper;

namespace Application.DTO.Carts
{
    public class CartDTO
    {
        public int Id { set; get; }
        public int product_id { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public string Size { set; get; }
        public string Loai { set; get; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { set; get; }
        public ProductDTO Products { get; set; }
        public ProductImage ProductImages { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Cart, CartDTO>();
            }
        }
    }
}
