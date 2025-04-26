using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
         public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.Categories.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
