using AutoMapper;

namespace Application.DTO.Categorys
{
    public class CreateUpdateCateoryRequest
    {
        public int type_id { get; set; }
        public string type_name { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateCateoryRequest, Entities.Category>();
            }
        }
    }
}
