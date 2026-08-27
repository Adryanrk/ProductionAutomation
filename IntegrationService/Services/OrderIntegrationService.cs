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

    public bool UpdateOrderStatus(int orderId, string newStatus)
    {
        try
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                Console.WriteLine($"Order {orderId} could not be found.");
                return false;
            }

            order.Status = newStatus;
            _context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error while updating order status: {ex.Message}");
            return false;
        }
    }
}