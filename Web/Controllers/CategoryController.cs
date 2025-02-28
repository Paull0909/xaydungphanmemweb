using Application.DTO.Categorys;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateCategory(CreateUpdateCateoryRequest request)
        {
            var category = _mapper.Map<CreateUpdateCateoryRequest, Category>(request);
            _unitOfWork.Categories.Add(category);
            
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else 
            {
                ViewBag.Category = "Loi vui long nhap lai";
                return RedirectToAction("CreateCategory");
            } 
        }
    }
}
