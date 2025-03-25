using Application.DTO.ProductImages;
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
        public async Task<IActionResult> Create(List<CreateUpdateProductImageRequest> list)
        {
            foreach(var i in list)
            {
                var img = _mapper.Map<CreateUpdateProductImageRequest, ProductImage>(i);
                 _unitOfWork.ProductImageRepository.Add(img);
            }
            _unitOfWork.CompleteAsync();
            return View();
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
