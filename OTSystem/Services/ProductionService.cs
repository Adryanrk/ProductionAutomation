using OTSystem.Models;

namespace OTSystem.Services;

public class ProductionService
{
    public void StartProduction(ProductionOrder order)
    {
        try
        {
            Console.WriteLine();
            Console.WriteLine($"Starting production for order {order.OrderId}...");
            Console.WriteLine($"Product: {order.ProductName}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine();

            order.Status = "Processing";

            for (int progress = 25; progress <= 100; progress += 25)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Production progress: {progress}%");
            }

            order.Status = "Completed";

            Console.WriteLine();
            Console.WriteLine($"Order {order.OrderId} completed successfully.");
        }
        catch (Exception ex)
        {
            order.Status = "Failed";
            Console.WriteLine($"Production error: {ex.Message}");
        }
    }
}