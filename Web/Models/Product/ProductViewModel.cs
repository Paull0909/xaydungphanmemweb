using Application.Enum;

namespace Web.Models.Product
{
    public class ProductViewModel
    {

        public int ProductId { get; set; }
        public string ?ProductName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public StatusProduct Status { get; set; }
        public string ?TypeName { get; set; }
        public string ?ImagePath { get; set; }
        public int? AdvertisementId { get; set; }
    }
}
