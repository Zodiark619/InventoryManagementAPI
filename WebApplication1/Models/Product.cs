namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 

        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int MinimumStock { get; set; }
        public string Unit { get; set; } = "Pcs";
       
        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }

        
    }

}
