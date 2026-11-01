namespace WebApplication1.Models.DTO.Supplier
{
    public class SupplierResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
    public class SupplierCreateDTO
    {
          public string Name { get; set; } = string.Empty;

    }
    public class SupplierUpdateDTO
    {
        public string Name { get; set; } = string.Empty;

    }
}
