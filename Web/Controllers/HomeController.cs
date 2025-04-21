using Application.DTO.Products;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Web.Models;
using System.Security.Claims;
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
            ViewBag.Category = await _unitOfWork.Categories.GetAllAsync();
            var pr = await _unitOfWork.Products.GetAllAsync();
            List<ProductDTO> product = new List<ProductDTO>();
            foreach (var item in pr)
            {
                var rr = _mapper.Map<Product, ProductDTO>(item);
                product.Add(rr);
            }
            foreach (var i in product)
            {
                i.img = await _unitOfWork.ProductImageRepository.GetImgByIdProductAsync(i.product_id);

            }
            if (product != null)
            {
                return View(product);
            }
            else
            {
                ViewBag.Product = "Khong co du lieu nao";
                return View();
            }
        }
        public async Task<IActionResult> InfoProduct(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            var pr = await _unitOfWork.Products.GetByIdAsync(id);
            ProductInfo product = _mapper.Map<Product, ProductInfo>(pr);
            product.imgs = await _unitOfWork.ProductImageRepository.GetListImgByIdProAsync(pr.product_id);
            product.variants = await _unitOfWork.VariantsProductRepository.GetByProduct(pr.product_id);
            foreach(var item in product.variants)
            {
                item.Size = await _unitOfWork.SizeProductsRepository.GetByProduct(item.Id);
            }
            if (product != null)
            {
                return View(product);
            }
            else
            {
                ViewBag.Product = "Khong co du lieu nao";
                return View();
            }
        }

        public async Task<IActionResult> ProductCate(int type_id)
        {
            var pr = await _unitOfWork.Products.GetProductofCate(type_id);
            List<ProductDTO> product = new List<ProductDTO>();
            foreach (var item in pr)
            {
                var rr = _mapper.Map<Product, ProductDTO>(item);
                product.Add(rr);
            }
            foreach (var i in product)
            {
                i.img = await _unitOfWork.ProductImageRepository.GetImgByIdProductAsync(i.product_id);

            }
            if (product != null)
            {
                return View(product);
            }
            else
            {
                ViewBag.Product = "Khong co du lieu nao";
                return View();
            }
        }
    }
}
