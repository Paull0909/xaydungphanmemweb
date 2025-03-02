namespace Application.Entities
{
    public class Size_Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int variants_product_id {  get; set; }
        public int quantity { get; set; }
        public Variants_product variants_product { get; set; }
    }
}
