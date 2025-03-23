using Application.DTO.Products;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Product;

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
            var products = await _unitOfWork.Products.GetAllAsync();
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var productsimg = await _unitOfWork.ProductImageRepository.GetAllAsync();
            // Check if products is null or empty
            if (products == null || !products.Any())
            {
                ViewBag.Message = "Không có sản phẩm nào";  
                return View(new List<ProductViewModel>());
            }

            // Proceed with the normal logic if products are found
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var advertisement = await _unitOfWork.AdventisementRepository.GetAllAsync();

            var model = new CreateUpdateProductRequest
            {
                Categories = categories,
                Advertisements=advertisement
            };

            return View(model); 
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
                    if (item.Size != null)
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
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Lấy danh mục và quảng cáo
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var advertisements = await _unitOfWork.AdventisementRepository.GetAllAsync();

            // Lấy danh sách hình ảnh của sản phẩm
            var productImages = await _unitOfWork.ProductImageRepository.GetListImgByIdProAsync(id);

            var variants = await _unitOfWork.VariantsProductRepository.GetAllAsync();
            var sizeproduct = await _unitOfWork.SizeProductsRepository.GetAllAsync();
            foreach (var variant in product.variants)
            {
                variant.Size = variant.Size.ToList();  // Chuyển ICollection<Size_Product> thành List<Size_Product>
            }

            var model = _mapper.Map<CreateUpdateProductRequest>(product);
            var products = _unitOfWork.Products.GetByIdAsync(id);
            // Kiểm tra lại danh sách Size đã được chuyển thành List
            foreach (var variant in model.variants)
            {
                Console.WriteLine($"Variant: {variant.Name}, Size Count: {variant.Size.Count}");
            }
            // Gán các giá trị bổ sung vào model
            model.Categories = categories;
            model.Advertisements = advertisements;
            model.img = productImages;
            model.variants = variants.ToList();

            return View(model);
        }

        [HttpPost]
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
