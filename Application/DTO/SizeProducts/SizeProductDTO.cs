using Application.DTO.Adventisements;
using AutoMapper;

namespace Application.DTO.SizeProducts
{
    public class SizeProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int variants_product_id { get; set; }
        public int quantity { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Size_Product, SizeProductDTO>();
            }
        }
    }
}
