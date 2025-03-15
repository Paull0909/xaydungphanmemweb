using Application.Entities;
using AutoMapper;

namespace Application.DTO.VariantsProducts
{
    public class CreateUpdateVariantsProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int product_id { get; set; }
        public List<Size_Product> size { get; set; }
        public List<ProductImage> images { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateVariantsProductRequest, Entities.Variants_product > ();
            }
        }
    }
}
