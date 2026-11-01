namespace WebApplication1.Models
{

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
       
    }

    public class Warehouse
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<WarehouseStock> Stocks { get; set; }
            = new List<WarehouseStock>();
    }

    public class WarehouseStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        public int Quantity { get; set; }

        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }

    public enum StockTransactionType
    {
        StockIn = 1,
        StockOut = 2,
        Adjustment = 3
    }

    public enum PurchaseOrderStatus
    {
        Draft,
        Pending,
        Approved,
        PartiallyReceived,
        Received,
        Cancelled
    }

    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;

        public int SupplierId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDate { get; set; }

        public PurchaseOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public ICollection<PurchaseOrderItem> Items { get; set; }
            = new List<PurchaseOrderItem>();
    }

    public class PurchaseOrderItem
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public int ReceivedQuantity { get; set; }

        public decimal UnitCost { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

    public class SalesOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<SalesOrderItem> Items { get; set; }
            = new List<SalesOrderItem>();
    }

    public class SalesOrderItem
    {
        public int Id { get; set; }

        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class StockTransaction
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        public StockTransactionType Type { get; set; }
        public int Quantity { get; set; }

        public decimal UnitCost { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? Notes { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
