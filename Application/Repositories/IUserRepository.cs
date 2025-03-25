using Application.DTO;
using Application.DTO.Users;
using Application.Entities;
using Application.SeedWorks;

namespace Application.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<PagedResult<UserDTO>> GetUserPagingAsync(PagedRequest request);
        
    }
}
