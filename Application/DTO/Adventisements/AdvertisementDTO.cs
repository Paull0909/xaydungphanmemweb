using Application.DTO.Categorys;
using AutoMapper;

namespace Application.DTO.Adventisements
{
    public class AdvertisementDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime create_at { get; set; }
        public int click { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool status { get; set; }
        public string img_path { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Advertisement, AdvertisementDTO>();
            }
        }
    }
}
