using Application.DTO.Products;
using Application.DTO.VariantsProducts;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Data.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class VariantController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public VariantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<CreateUpdateVariantsProductRequest> request)
        {
            foreach(var item in request)
            {
                var variants = _mapper.Map<CreateUpdateVariantsProductRequest, Application.Entities.Variants_product>(item);
                _unitOfWork.VariantsProductRepository.Add(variants);
                foreach(var sz in item.size)
                {
                    _unitOfWork.SizeProductsRepository.Add(sz);
                }
            }
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                ViewBag.Product = "Them thanh cong";
                return RedirectToAction("GetAllProduct");
            }
            else
            {
                ViewBag.Product = "Loi vui long nhap lai";
                return RedirectToAction("Create");
            }
        }
    }
}
