using AutoMapper;

namespace Application.DTO.SizeProducts
{
    public class CreateUpdateSizeProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int variants_product_id { get; set; }
        public int quantity { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateSizeProductRequest, Entities.Size_Product > ();
            }
        }
    }
}
