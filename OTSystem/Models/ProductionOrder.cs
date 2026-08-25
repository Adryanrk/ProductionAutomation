namespace OTSystem.Models;

public class ProductionOrder
{
    public int OrderId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string Status { get; set; } = "Waiting";
}