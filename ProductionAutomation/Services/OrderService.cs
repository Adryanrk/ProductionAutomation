using OrderSystem.Data;
using OrderSystem.Models;

namespace OrderSystem.Services;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public bool CreateOrder(string productName, int quantity)
    {
        var order = new Order
        {
            ProductName = productName,
            Quantity = quantity
        };

        var validator = new OrderValidator();

        if (!validator.IsValid(order))
        {
            return false;
        }

        try
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
            return false;
        }
    }

    public List<Order> GetAllOrders()
    {
        try
        {
            return _context.Orders.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
            return new List<Order>();
        }
    }

}