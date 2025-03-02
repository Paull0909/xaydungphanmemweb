using Application.DTO.Products;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create(CreateUpdateProductRequest request)
        {
            var product = _mapper.Map<CreateUpdateProductRequest, Product>(request);
            _unitOfWork.Products.Add(product);

            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return RedirectToAction("Create");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct()
        {
            return View();
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> EditProduct(int id, CreateUpdateProductRequest request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _mapper.Map(request, product);
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return RedirectToAction("EditProduct");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct()
        {
            return View();
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> DeleteProduct(int[] ids)
        {
            foreach (var id in ids)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                _unitOfWork.Products.Remove(product);
            }
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return RedirectToAction("DeleteProduct");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return View();
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _unitOfWork.Products.GetAllAsync();
            if (product != null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            // return Ok(category);
            else
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return RedirectToAction("GetProducts");
            }

        }
    }
}
