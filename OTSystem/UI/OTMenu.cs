using OTSystem.Models;
using OTSystem.Services;

namespace OTSystem.UI;

public class OTMenu
{
    private readonly ProductionService _productionService;

    public OTMenu(ProductionService productionService)
    {
        _productionService = productionService;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("============================");
            Console.WriteLine("       OT SYSTEM");
            Console.WriteLine("============================");
            Console.WriteLine("1. Simulate production order");
            Console.WriteLine("2. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SimulateOrder();
                    break;

                case "2":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void SimulateOrder()
    {
        Console.Write("Order ID: ");

        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid order ID.");
            return;
        }

        Console.Write("Product name: ");
        string productName = Console.ReadLine() ?? string.Empty;

        Console.Write("Quantity: ");

        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (string.IsNullOrWhiteSpace(productName) || quantity <= 0)
        {
            Console.WriteLine("Invalid production order.");
            return;
        }

        var order = new ProductionOrder
        {
            OrderId = orderId,
            ProductName = productName,
            Quantity = quantity
        };

        _productionService.StartProduction(order);
    }
}