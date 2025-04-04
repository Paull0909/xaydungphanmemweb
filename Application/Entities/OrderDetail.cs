﻿namespace Application.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int bill_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public decimal Price { set; get; }
        public string Cata_product { set; get; }
        public string Size {  set; get; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
