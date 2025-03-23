using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.SeedWorks;
using AutoMapper;
using Web.Models.Product;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var productImages = await _unitOfWork.ProductImageRepository.GetAllAsync();

            if (products == null || !products.Any())
            {
                ViewBag.Message = "Không có sản phẩm nào";
                return View(new List<ProductViewModel>());
            }

            var productViewModels = products.Select(p => new ProductViewModel
            {
                ProductId = p.product_id,
                ProductName = p.product_name,
                Price = p.price,
                Discount = p.discount,
                Status = p.status,
                TypeName = p.Category?.type_name ?? "Không xác định", 
                ImagePath = !string.IsNullOrEmpty(p.ProductImages?.FirstOrDefault()?.ImagePath)
                    ? p.ProductImages.FirstOrDefault()?.ImagePath
                    : "default.jpg"  
            }).ToList();
            return View(productViewModels);
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}