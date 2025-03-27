using Application.DTO.Adventisements;
using Application.DTO.ProductImages;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AdventisementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AdventisementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Create(CreateUpdateAdvertisementRequest ad)
        {
                var adv = _mapper.Map<CreateUpdateAdvertisementRequest, Advertisement>(ad);
                _unitOfWork.AdventisementRepository.Add(adv);
                _unitOfWork.CompleteAsync();
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var adv = await _unitOfWork.AdventisementRepository.GetByIdAsync(id);
            _unitOfWork.AdventisementRepository.Remove(adv);
            _unitOfWork.CompleteAsync();
            return View();
        }
    }
}
