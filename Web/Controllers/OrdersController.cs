using Application.DTO.OrderDetails;
using Application.DTO.Orders;
using Application.DTO.Products;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllByIdUser(Guid id)
        {
            var or = _unitOfWork.OrderRepository.GetAllByUser(id);
            return View(or);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByBillId(int id)
        {
            var ord = _unitOfWork.OrderDetailRepository.GetAllByBill(id);
            return View(ord);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrdersByProduct(CreateUpdateOrderDetailRequest a)
        {
            var img = await _unitOfWork.ProductImageRepository.GetListImgByIdProAsync(a.product_id);
            var name = await _unitOfWork.Products.GetByIdAsync(a.product_id);
            a.name_product = name.product_name;
            a.Images = img;
            return View(a);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrdersByProduct(CreateUpdateOrderDetailRequest a, CreateUpdateOrderRequest od)
        {
            if (a != null || od  != null)
            {
                var or = _mapper.Map<CreateUpdateOrderRequest, Order>(od);
                _unitOfWork.OrderRepository.Add(or);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    a.product_id = or.bill_id;
                    var ord = _mapper.Map<CreateUpdateOrderDetailRequest, OrderDetail>(a);
                    _unitOfWork.OrderDetailRepository.Add(ord);
                    TotalRevenue t = new TotalRevenue();
                    var total = await _unitOfWork.TotalRevenueRepository.GetByDate(DateTime.Now);
                    if (total != null)
                    {
                        total.tongdoanhthu += or.Totalprice;
                        total.numberofsales += 1;
                    }
                    else
                    {
                        t.tongdoanhthu = or.Totalprice;
                        t.date = DateTime.Now;
                        t.numberofsales = 1;
                    }
                    _unitOfWork.CompleteAsync();
                    return View(a);
                }
                else
                {
                    ViewBag.Orders = "Mua hang khong thanh cong";
                    return View(a);
                }
            }
            else
            {
                ViewBag.Orders = "Mua hang khong thanh cong";
                return View(a);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrdersByCart(List<int> id)
        {
            if (id.Count>0)
            {
                List<Cart> list = new List<Cart>();
                foreach (var item in id)
                {
                    var cart = await _unitOfWork.CartRepository.GetByIdAsync(item);
                    list.Add(cart);
                }
                return View(list);
            }
            else
            {
                ViewBag.Orders = "Vui long chon san pham";
                return RedirectToAction("GetAllById", "Cart");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrdersByCart(List<CreateUpdateOrderDetailRequest> list, CreateUpdateOrderRequest od)
        {
            if (list != null || od  != null)
            {
                var or = _mapper.Map<CreateUpdateOrderRequest, Order>(od);
                _unitOfWork.OrderRepository.Add(or);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    foreach (var item in list)
                    {
                        item.product_id = or.bill_id;
                        var ord = _mapper.Map<CreateUpdateOrderDetailRequest, OrderDetail>(item);
                        _unitOfWork.OrderDetailRepository.Add(ord);
                    }
                    _unitOfWork.CompleteAsync();
                    return View(result);
                }
                else
                {
                    ViewBag.Orders = "Mua hang khong thanh cong";
                    return View(list);
                }
            }
            else
            {
                ViewBag.Orders = "Mua hang khong thanh cong";
                return View(list);
            }
        }
    }
}
