using Application.DTO.Categorys;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.Enum;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Products
{
    public class ProductDTO
    {
        public int product_id { get; set; }
        public string product_name { get; set; }

        public decimal price { get; set; }
        public int discount { get; set; }
        public StatusProduct status { get; set; }
        public int IsFeatured { get; set; }
        public int type_id { get; set; }
        public string Desdescription { get; set; }
        public int advertisement_id { get; set; }
        public int soluong {  get; set; }
        public ProductImage img { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Product, ProductDTO>();
            }
        }
    }
}
