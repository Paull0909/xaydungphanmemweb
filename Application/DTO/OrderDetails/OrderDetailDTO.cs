using Application.DTO.Adventisements;
using Application.DTO.ProductImages;
using AutoMapper;

namespace Application.DTO.OrderDetails
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int bill_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public decimal Price { set; get; }
        public string Cata_product { set; get; }
        public string Size { set; get; }
        public ProductImageDTO imageDTO { set; get; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Order, OrderDetailDTO>();
            }
        }
    }
}
