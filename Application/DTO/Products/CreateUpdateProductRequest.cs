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
    public class CreateUpdateProductRequest
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
        public List<ProductImage> img { set; get; }
        public List<string> size { get; set; }
        public int quality { get; set; }
        public string Caption { get; set; }
        public List<Variants_product> variants { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }

        // Ánh xạ từ CreateUpdateProductRequest đến Product
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateProductRequest, Entities.Product>()
                    .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.img)) // Ánh xạ ProductImage từ DTO sang Entity
                    .ForMember(dest => dest.variants, opt => opt.MapFrom(src => src.variants)); // Ánh xạ Variants từ DTO sang Entity

                // Ánh xạ ngược từ Product về CreateUpdateProductRequest (nếu cần)
                CreateMap<Entities.Product, CreateUpdateProductRequest>()
                    .ForMember(dest => dest.img, opt => opt.MapFrom(src => src.ProductImages)) // Ánh xạ ProductImage từ Entity sang DTO
                    .ForMember(dest => dest.variants, opt => opt.MapFrom(src => src.variants)); // Ánh xạ Variants từ Entity sang DTO
            }
        }
    }
}
