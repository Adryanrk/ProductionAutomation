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

        bool processingUpdated =
            integrationService.UpdateOrderStatus(
                order.Id,
                "Processing");

        if (!processingUpdated)
        {
            Console.WriteLine(
                $"Could not update order {order.Id} to Processing.");

            continue;
        }

        string? result = otClient.SendOrder(order);

        if (result == "Completed")
        {
            Console.WriteLine(
                $"Order {order.Id} completed successfully in OT system.");

            bool statusUpdated =
                integrationService.UpdateOrderStatus(
                    order.Id,
                    "Completed");

            if (statusUpdated)
            {
                Console.WriteLine(
                    $"Order {order.Id} status updated to Completed.");
            }
            else
            {
                Console.WriteLine(
                    $"Could not update status for order {order.Id}.");
            }
        }
        else if (result == "Failed")
        {
            Console.WriteLine(
                $"Order {order.Id} failed in OT system.");

            integrationService.UpdateOrderStatus(
                order.Id,
                "Failed");
        }
        else
        {
            Console.WriteLine(
                $"Could not confirm production for order {order.Id}.");

            integrationService.UpdateOrderStatus(
                order.Id,
                "Pending");
        }

        Console.WriteLine();
    }
}