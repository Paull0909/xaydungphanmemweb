using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TotalRevenueController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TotalRevenueController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var total = await _unitOfWork.TotalRevenueRepository.GetAllforYear();
            return View(total);
        }
        [HttpGet]
        public async Task<IActionResult> GetTotalbyYear(int year)
        {
            var total = await _unitOfWork.TotalRevenueRepository.GetDetailforYear(year);
            return View(total);
        }
    }
}
