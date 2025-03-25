using Application.DTO;
using Application.DTO.Users;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.Repositories;
using AutoMapper;
using Data.EF;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(WebDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<UserDTO>> GetUserPagingAsync(PagedRequest request)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(x => x.Email.Contains(request.search)
                                        || x.UserName.Contains(request.search)
                                        || x.PhoneNumber.Contains(request.search));
            }
            //Query page
            query = query
                .OrderByDescending(x => x.Id)
                .Skip((request.page - 1) * request.size)
                .Take(request.size);
            var list = query.ToListAsync();
            var rowCount = list.Result.Count();
            List<UserDTO>  userDtos = new List<UserDTO>();
            list.Result.ForEach((x) =>
            {
                userDtos.Add(new UserDTO
                {
                    Id = x.Id,
                    Dob = x.Dob,
                    UserName=x.UserName,
                    Email = x.Email 
                });
            });

            var pagedResponse = new PagedResult<UserDTO>
            {
                Data = userDtos,
                CurrentPage = request.page,
                RowCount = rowCount,
                PageSize = request.size,
            };
            return pagedResponse;
        }

       
    }
}
