using AutoMapper;

namespace Application.DTO.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; }
        public DateTime Dob {  get; set; }  
        public string Email { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.User, UserDTO>();
            }
        }

    }
}
