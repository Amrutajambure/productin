namespace productin.Models
{
    public class Product
    {
        public int Id { get; set; } // Ensure you have an Id property
        public string Name { get; set; } = string.Empty; // Use a default value to avoid warnings
        public decimal Price { get; set; }
    }
}