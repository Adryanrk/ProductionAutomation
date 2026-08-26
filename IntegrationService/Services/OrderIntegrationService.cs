using IntegrationService.Data;
using IntegrationService.Models;

namespace IntegrationService.Services;

public class OrderIntegrationService
{
    private readonly IntegrationDbContext _context;

    public OrderIntegrationService(IntegrationDbContext context)
    {
        _context = context;
    }

    public List<IntegrationOrder> GetPendingOrders()
    {
        try
        {
            return _context.Orders
                .Where(order => order.Status == "Pending")
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
            return new List<IntegrationOrder>();
        }
    }
}