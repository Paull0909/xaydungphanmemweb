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
        public List<Variants_product> variants { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateProductRequest, Entities.Product > ();
            }
        }
    }
}
