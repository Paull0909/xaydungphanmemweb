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

        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _unitOfWork.Products.GetAllAsync();
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var catagory = await _unitOfWork.Categories.GetAllAsync();
            return View(catagory);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateProductRequest request)
        {
            var product = _mapper.Map<CreateUpdateProductRequest, Product>(request);
            _unitOfWork.Products.Add(product);
            if (request.img != null)
            {
                foreach (var item in request.img)
                {
                    item.product_id = product.product_id;
                    _unitOfWork.ProductImageRepository.Add(item);
                }
            }
            if(request.variants != null)
            {
                foreach (var item in request.variants)
                {
                    item.product_id = product.product_id;
                    _unitOfWork.VariantsProductRepository.Add(item);
                    if(item.Size != null)
                    {
                        foreach (var s in item.Size)
                        {
                            s.variants_product_id = item.Id;
                            _unitOfWork.SizeProductsRepository.Add(s);
                        }
                    }
                }
            }
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return RedirectToAction("GetAllProduct");
            }
            else
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return RedirectToAction("Create");
            }
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            ViewBag.Category = await _unitOfWork.Categories.GetAllAsync();
            product.ProductImages = await _unitOfWork.ProductImageRepository.GetListImgByIdProAsync(id);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(CreateUpdateProductRequest request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.product_id);
            if (product == null)
            {
                ViewBag.Product = "Khong tim thay san pham";
                return View("EditProduct");
            }
            _mapper.Map(request, product);
            if(product.ProductImages != null)
            {
                foreach (var image in product.ProductImages)
                {
                    var imgs = _unitOfWork.ProductImageRepository.GetByIdAsync(image.Id);
                    if(imgs != null)
                    {
                        _mapper.Map(image, imgs);
                    }         
                }
            }
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return RedirectToAction("EditProduct");
            }
            else
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return RedirectToAction("EditProduct");
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return NotFound();
            }
            _unitOfWork.Products.Remove(product);
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                ViewBag.Product = "Xoa san pham thanh cong";
                return RedirectToAction("GetAllProduct");
            }
            else
            {
                ViewBag.Product = "Xoa san pham khong thanh cong";
                return RedirectToAction("GetAllProduct");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductInfo(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            product.variants = await _unitOfWork.VariantsProductRepository.GetByProduct(product.product_id);
            foreach (var item in product.variants)
            {
                item.Size = await _unitOfWork.SizeProductsRepository.GetByProduct(item.Id);
            }
            return View(product);
        }
    }
}
