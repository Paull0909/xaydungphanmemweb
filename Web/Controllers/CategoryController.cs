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
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateUpdateCateoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                TempData["Category"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại!";
                return RedirectToAction("CreateCategory"); 
            }

            var category = _mapper.Map<CreateUpdateCateoryRequest, Category>(request);
            _unitOfWork.Categories.Add(category);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                TempData["Category"] = "Thêm thành công!";
                return RedirectToAction("GetAllCategory"); 
            }
            else
            {
                TempData["Category"] = "Có lỗi xảy ra. Vui lòng thử lại!";
                return RedirectToAction("CreateCategory"); 
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditCategory(int type_id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(type_id);

            if (category == null)
            {
                TempData["CategoryMessage"] = "Không tìm thấy phân loại sản phẩm bạn chọn.";
                return RedirectToAction("GetAllCategory");
            }

            var request = _mapper.Map<CreateUpdateCateoryRequest>(category);

            return View(request);  
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(CreateUpdateCateoryRequest request)
        {
            if (request == null)
            {
                ViewBag.Category = "Dữ liệu không hợp lệ!";
                return View();
            }
            var category = await _unitOfWork.Categories.GetByIdAsync(request.type_id);

            if (category == null)
            {
                ViewBag.Category = "Danh mục không tồn tại.";
                return RedirectToAction("GetAllCategory");
            }
            _mapper.Map(request, category);
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                TempData["Category"] = "Cập nhật thành công!";
                return RedirectToAction("GetAllCategory");
            }
            TempData["Category"] = "Cập nhật không thành công. Vui lòng thử lại.";
            return RedirectToAction("GetAllCategory");
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
                TempData["Category"] = "Xoa thang cong";
                return RedirectToAction("GetAllCategory");
            }
            else
            {
                TempData["Category"] = "Xoa khong thanh cong";
                return RedirectToAction("DeleteCategory");
            }
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<CategoryDTO>>> GetCategoryPaging(PagedRequest request)
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