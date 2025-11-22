namespace SalesManagement.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ProductType>? ProductTypes { get; set; }
    }
}
