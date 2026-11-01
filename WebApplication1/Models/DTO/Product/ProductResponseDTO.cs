    namespace WebApplication1.Models.DTO.Product
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int MinimumStock { get; set; }
        public string Unit { get; set; } = "Pcs";
        public string CategoryName { get; set; } = string.Empty;
        public string? SupplierName { get; set; }

       

    }
    public class ProductUpdateDTO
    {
          public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int MinimumStock { get; set; }
        public string Unit { get; set; } = "Pcs";

       

    }
    public class ProductCreateDTO
    {
        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int MinimumStock { get; set; }
        public string Unit { get; set; } = "Pcs";

       

    }
}
