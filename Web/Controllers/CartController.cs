using Application.DTO.Carts;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            var cart = await _unitOfWork.CartRepository.GetAllByUser(id);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id);
            if(cart == null)
            {
                ViewBag.Cart = "Xoa that bai";
                return View("GetAllById");
            }
            _unitOfWork.CartRepository.Remove(cart);
            return RedirectToAction("GetAllById");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(CreateUpdateCartRequest request)
        {
            if (request == null)
            {
                var cart = _mapper.Map<CreateUpdateCartRequest, Cart>(request);
                _unitOfWork.CartRepository.Add(cart);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    ViewBag.Category = "Da them vao gio hang";
                    return RedirectToAction("GetAllCategory");
                }
                else
                {
                    ViewBag.Category = "Loi vui long nhap lai";
                    return RedirectToAction("CreateCategory");
                }
            }
            else
            {
                ViewBag.Cart = "Them san pham that bai vui long chon lai";
                return View();
            }                
        }
    }
}
