using Application.DTO.Transactions;
using AutoMapper;

namespace Application.DTO.VariantsProducts
{
    public class VariantsProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int product_id { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Variants_product, VariantsProductDTO>();
            }
        }
    }
}
