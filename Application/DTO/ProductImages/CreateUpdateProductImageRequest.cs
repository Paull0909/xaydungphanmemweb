using AutoMapper;

namespace Application.DTO.ProductImages
{
    public class CreateUpdateProductImageRequest
    {
        public int Id { get; set; }
        public int product_id { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public DateTime DateCreated { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateProductImageRequest, Entities.ProductImage>();
            }
        }
    }
}
