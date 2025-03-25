using Application.DTO.ProductImages;
using Application.DTO.VariantsProducts;
using Application.Entities;

namespace Application.DTO.Products
{
    public class ProductBuyer
    {
        public int product_id { get; set; }
        public string product_name { get; set; }

        public decimal price { get; set; }
        public int type_id { get; set; }
        public int soluong { get; set; }
        public string size { get; set; }
        public string variant { get; set; }
        public ProductImage img { get; set; }
    }
}
