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
        [HttpPost]
        public async Task<IActionResult> Create(List<CreateUpdateVariantsProductRequest> request)
        {
            foreach (var item in request)
            {
                var varian = _mapper.Map<CreateUpdateVariantsProductRequest, Variants_product>(item);
                _unitOfWork.VariantsProductRepository.Add(varian);
                foreach (var i in item.sizes)
                {
                    i.variants_product_id = varian.Id;
                    _unitOfWork.SizeProductsRepository.Add(i);
                }
                foreach (var j in item.images)
                {
                    j.product_id = varian.product_id;
                    _unitOfWork.ProductImageRepository.Add(j);
                }
            }
            var resul = await _unitOfWork.CompleteAsync();
            return View(resul);
        }
    }
}
