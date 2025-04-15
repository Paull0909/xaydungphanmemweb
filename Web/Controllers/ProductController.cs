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
        public async Task<IActionResult> GetAllProduct()
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
                i.category = await _unitOfWork.Categories.GetCategoryByIdAsync(i.type_id);
                var varent = await _unitOfWork.VariantsProductRepository.GetByProduct(i.product_id);
                foreach (var j in varent)
                {
                    var size = await _unitOfWork.SizeProductsRepository.GetByProduct(j.Id);
                    i.soluong = size.Sum(t => t.quantity);
                }
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var advertisement = await _unitOfWork.AdventisementRepository.GetAllAsync();

            var model = new CreateUpdateProductRequest
            {
                Categories = categories,
                Advertisements = advertisement
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateProductRequest request)
        {
            var product = _mapper.Map<CreateUpdateProductRequest, Product>(request);
            _unitOfWork.Products.Add(product);          
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
            var viewModel = _mapper.Map<CreateUpdateProductRequest>(product);
            viewModel.Categories = await _unitOfWork.Categories.GetAllAsync();
            viewModel.Advertisements = await _unitOfWork.AdventisementRepository.GetAllAsync();
            viewModel.ProductImages = await _unitOfWork.ProductImageRepository.GetListImgByIdProAsync(id);
            viewModel.variants = await _unitOfWork.VariantsProductRepository.GetVariantsByProductIdAsync(id);
            return View(viewModel);
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
                    var imgs =  await _unitOfWork.ProductImageRepository.GetByIdAsync(image.Id);
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
