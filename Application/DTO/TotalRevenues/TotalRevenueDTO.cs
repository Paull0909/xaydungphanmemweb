using Application.DTO.SizeProducts;
using AutoMapper;

namespace Application.DTO.TotalRevenues
{
    public class TotalRevenueDTO
    {
        public int doanhthu_id { get; set; }
        public DateTime date { get; set; }
        public int numberofsales { get; set; }
        public decimal tongdoanhthu { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.TotalRevenue, TotalRevenueDTO>();
            }
        }
    }
}
