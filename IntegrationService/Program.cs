using IntegrationService.Data;
using IntegrationService.Services;

using var context = new IntegrationDbContext();

var integrationService = new OrderIntegrationService(context);
var otClient = new OTClient();

Console.WriteLine("============================");
Console.WriteLine("    INTEGRATION SERVICE");
Console.WriteLine("============================");
Console.WriteLine();
Console.WriteLine("Scanning for pending orders...");
Console.WriteLine();

var pendingOrders = integrationService.GetPendingOrders();

if (pendingOrders.Count == 0)
{
    Console.WriteLine("No pending orders found.");
}
else
{
    foreach (var order in pendingOrders)
    {
        Console.WriteLine($"Order found: {order.Id}");
        Console.WriteLine($"Product: {order.ProductName}");
        Console.WriteLine($"Quantity: {order.Quantity}");
        Console.WriteLine();

        Console.WriteLine("Sending order to OT system...");

        bool success = otClient.SendOrder(order);

        if (success)
        {
            Console.WriteLine($"Order {order.Id} sent successfully.");

            bool statusUpdated =
                integrationService.UpdateOrderStatus(order.Id, "Processing");

            if (statusUpdated)
            {
                Console.WriteLine($"Order {order.Id} status updated to Processing.");
            }
            else
            {
                Console.WriteLine($"Could not update status for order {order.Id}.");
            }
        }
        else
        {
            Console.WriteLine($"Failed to send order {order.Id}.");
        }

        Console.WriteLine();
    }
}