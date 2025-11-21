namespace SalesManagement.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Product>? Products { get; set; }
    }
}
