using IntegrationService.Data;
using IntegrationService.Services;

using var context = new IntegrationDbContext();

var integrationService = new OrderIntegrationService(context);

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
        Console.WriteLine($"Order found:");
        Console.WriteLine($"ID: {order.Id}");
        Console.WriteLine($"Product: {order.ProductName}");
        Console.WriteLine($"Quantity: {order.Quantity}");
        Console.WriteLine($"Status: {order.Status}");
        Console.WriteLine();
    }
}