namespace Application.Entities
{
    public class Category
    {
        public int type_id { get; set; }
        public string type_name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
