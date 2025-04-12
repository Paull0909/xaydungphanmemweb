using Application.DTO.Products;
using Application.DTO.SizeProducts;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SizeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SizeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSize(CreateUpdateSizeProductRequest request)
        {
            var model = _mapper.Map<CreateUpdateSizeProductRequest, Size_Product>(request);
            var name = await _unitOfWork.SizeProductsRepository.GetByProduct(model.variants_product_id);
            string mess = "Size da ton tai";
            foreach(var i in name)
            {
                if (i.Name == model.Name) return View(mess);
            }
            _unitOfWork.SizeProductsRepository.Add(model);
            _unitOfWork.CompleteAsync();
            return View(model);
        }
    }
}
