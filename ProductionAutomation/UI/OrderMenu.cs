using OrderSystem.Services;

namespace OrderSystem.UI;

public class OrderMenu
{
    private readonly OrderService _orderService;

    public OrderMenu(OrderService orderService)
    {
        _orderService = orderService;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("============================");
            Console.WriteLine("     ORDER MANAGEMENT");
            Console.WriteLine("============================");
            Console.WriteLine("1. List orders");
            Console.WriteLine("2. Create new order");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowOrders();
                    break;

                case "2":
                    CreateOrder();
                    break;

                case "3":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void ShowOrders()
    {
        var orders = _orderService.GetAllOrders();

        Console.WriteLine();
        Console.WriteLine("ORDERS");
        Console.WriteLine("----------------------------");

        if (orders.Count == 0)
        {
            Console.WriteLine("No orders found.");
            return;
        }

        foreach (var order in orders)
        {
            Console.WriteLine(
                $"ID: {order.Id} | " +
                $"Product: {order.ProductName} | " +
                $"Quantity: {order.Quantity} | " +
                $"Status: {order.Status} | " +
                $"Created: {order.CreatedAt}");
        }
    }

    private void CreateOrder()
    {
        Console.Write("Product name: ");
        string productName = Console.ReadLine() ?? string.Empty;

        Console.Write("Quantity: ");

        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid quantity. Please enter a number.");
            return;
        }

        bool success = _orderService.CreateOrder(productName, quantity);

        if (success)
        {
            Console.WriteLine("Order created successfully.");
        }
        else
        {
            Console.WriteLine("Order could not be created.");
            Console.WriteLine("Make sure the product name is not empty and quantity is greater than 0.");
        }
    }
}