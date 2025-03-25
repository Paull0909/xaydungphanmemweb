using Application.DTO.Products;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class Variant_ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public Variant_ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            var request = new CreateUpdateVariantsProductRequest
            {
                product_id = product.product_id,
            };

            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<CreateUpdateVariantsProductRequest> request)
        {
            foreach (var item in request)
            {

                var varian = _mapper.Map<CreateUpdateVariantsProductRequest, Variants_product>(item);
                _unitOfWork.VariantsProductRepository.Add(varian);
                await _unitOfWork.CompleteAsync();
                foreach (var i in item.sizes)
                {
                    i.variants_product_id = varian.Id;
                    _unitOfWork.SizeProductsRepository.Add(i);
                }
            }
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("GetAllProduct", "Product");
        }
    }
}
