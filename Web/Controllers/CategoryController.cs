using Application.DTO;
using Application.DTO.Categorys;
using Application.Entities;
using Application.SeedWorks;
using Application.Services;
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
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateUpdateCateoryRequest request)
        {
            var category = _mapper.Map<CreateUpdateCateoryRequest, Category>(request);
            _unitOfWork.Categories.Add(category);
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                ViewBag.Category = "Them thanh cong";
                return RedirectToAction("GetAllCategory");
            }
            else 
            {
                ViewBag.Category = "Loi vui long nhap lai";
                return RedirectToAction("CreateCategory");
            } 
        }
        [HttpGet]
        public async Task<IActionResult> EditCategory()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditCategory(CreateUpdateCateoryRequest request)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.type_id);
            if (category == null)
            {
                ViewBag.Category = "Khong thay phan loai san pham vua chon";
                return RedirectToAction("GetAllCategory");
            }
            _mapper.Map(request, category);
            var result = await _unitOfWork.CompleteAsync();
            return RedirectToAction("EditCategory");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
             var category = await _unitOfWork.Categories.GetByIdAsync(id);
             if (category == null)
             {
                return NotFound();
             }
            _unitOfWork.Categories.Remove(category);
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                ViewBag.Category = "Xoa thang cong";
                return RedirectToAction("GetAllCategory");
            }
            else
            {
                ViewBag.Category = "Xoa khong thang cong";
                return RedirectToAction("DeleteCategory");
            }
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<CategoryDTO>>> GetCategoryPaging( PagedRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                var result = await _unitOfWork.Categories.GetCategoryPagingAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
