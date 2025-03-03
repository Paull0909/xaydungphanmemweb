using Application.Enum;
using Application.Entities;
using AutoMapper;

namespace Application.DTO.Orders
{
    public class CreateUpdateOrderRequest
    {
        public int bill_id { set; get; }
        public DateTime OrderDate { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public decimal Totalprice { set; get; }
        public decimal ShippingFee { set; get; }
        public string Note { set; get; }
        public Status Status { set; get; }
        public TransactionStatus transactionStatus { set; get; }
        public Guid UserId { set; get; }
        public List<Product> Products { set; get; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateOrderRequest, Entities.Order > ();
            }
        }
    }
}
