using Application.DTO.ProductImages;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ImageProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ImageProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Create([FromQuery] int product_id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(product_id);
            var request = new CreateUpdateProductImageRequest
            {
                product_id = product.product_id,
            };

            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<CreateUpdateProductImageRequest> list)
        {
            foreach(var i in list)
            {
                i.DateCreated = DateTime.UtcNow;
                var img = _mapper.Map<CreateUpdateProductImageRequest, ProductImage>(i);
                 _unitOfWork.ProductImageRepository.Add(img);
            }
           await _unitOfWork.CompleteAsync();
            return RedirectToAction("GetAllProduct", "Product");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var img = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
            _unitOfWork.ProductImageRepository.Remove(img);
            _unitOfWork.CompleteAsync();
            return View();
        }
    }
}
