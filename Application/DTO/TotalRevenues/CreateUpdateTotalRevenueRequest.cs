using AutoMapper;

namespace Application.DTO.TotalRevenues
{
    public class CreateUpdateTotalRevenueRequest
    {
        public int doanhthu_id { get; set; }
        public DateTime date { get; set; }
        public int numberofsales { get; set; }
        public decimal tongdoanhthu { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateTotalRevenueRequest, Entities.TotalRevenue > ();
            }
        }
    }
}
