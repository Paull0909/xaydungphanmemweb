using Application.DTO.Carts;
using Application.DTO.OrderDetails;
using Application.DTO.Orders;
using Application.DTO.ProductImages;
using Application.DTO.Products;
using Application.Entities;
using Application.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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

        [HttpGet]
        public async Task<IActionResult> GetAllByIdUser(Guid id)
        {
            var or = await _unitOfWork.OrderRepository.GetAllByUser(id);
            return View(or);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllByBillId(int id)
        {
            var list = await _unitOfWork.OrderDetailRepository.GetAllByBill(id);
            List<OrderDetailDTO> orderDetails = new List<OrderDetailDTO>();
            foreach (var i in list)
            {
                var odt = i;
                var ord = _mapper.Map<OrderDetail,OrderDetailDTO>(odt);
                var pr = await _unitOfWork.Products.GetByIdAsync(ord.product_id);
                ord.product_name = pr.product_name;
                orderDetails.Add(ord);
            }               
            foreach(var i in orderDetails)
            {
                var img = await _unitOfWork.ProductImageRepository.GetImgByIdProductAsync(i.product_id);
                i.imageDTO = _mapper.Map<ProductImage, ProductImageDTO>(img);
            }
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrdersByProduct(ProductBuyer a)
        {
            a.img = await _unitOfWork.ProductImageRepository.GetImgByIdProductAsync(a.product_id);
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
                    a.bill_id = or.bill_id;
                    var ord = _mapper.Map<CreateUpdateOrderDetailRequest, OrderDetail>(a);
                    _unitOfWork.OrderDetailRepository.Add(ord);
                    var variant = await _unitOfWork.VariantsProductRepository.Loadwhenbuyer(ord.product_id, ord.Cata_product);
                    var size = await _unitOfWork.SizeProductsRepository.Loadwhenbuyer(variant.Id, ord.Size, ord.quantity);
                    if(size == false)
                    {
                        return View("CreateOrdersByProduct");
                    }
                    TotalRevenue t = new TotalRevenue
                    {
                        tongdoanhthu = or.Totalprice,
                        date = DateTime.Now,
                        numberofsales = 1
                    };
                    await _unitOfWork.TotalRevenueRepository.AddWhenbyOder(t);
                    _unitOfWork.CompleteAsync();
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Orders = "Loi khi mua hang";
                return View(a);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrdersByCart(List<int> id)
        {
            if (id.Count>0)
            {
                List<CartDTO> list = new List<CartDTO>();
                foreach (var item in id)
                {
                    var i = await _unitOfWork.CartRepository.GetByIdAsync(item);
                    var cart = _mapper.Map<Cart, CartDTO>(i);
                    var pr = await _unitOfWork.Products.GetByIdAsync(cart.product_id);
                    cart.Products = _mapper.Map<Product, ProductDTO>(pr);
                    cart.ProductImages = await _unitOfWork.ProductImageRepository.GetImgByIdProductAsync(cart.product_id);
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
                        item.bill_id = or.bill_id;
                        var ord = _mapper.Map<CreateUpdateOrderDetailRequest, OrderDetail>(item);
                        _unitOfWork.OrderDetailRepository.Add(ord);
                        var variant = await _unitOfWork.VariantsProductRepository.Loadwhenbuyer(ord.product_id, ord.Cata_product);
                        var size = await _unitOfWork.SizeProductsRepository.Loadwhenbuyer(variant.Id, ord.Size, ord.quantity);
                        if (size == false)
                        {
                            return View("CreateOrdersByProduct");
                        }
                    }
                    TotalRevenue t = new TotalRevenue
                    {
                        tongdoanhthu = or.Totalprice,
                        date = DateTime.Now,
                        numberofsales = 1
                    };
                    await _unitOfWork.TotalRevenueRepository.AddWhenbyOder(t);
                    _unitOfWork.CompleteAsync();
                    return RedirectToAction("Index", "Home");
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
        [HttpGet]
        public async Task<IActionResult> GetBillNew()
        {
            var or = await _unitOfWork.OrderRepository.GetAllByBillNew();
            return View(or);
        }

        [HttpGet]
        public async Task<IActionResult> GetBillOld()
        {
            var or = await _unitOfWork.OrderRepository.GetAllByBillOld();
            return View(or);
        }
    }
}
