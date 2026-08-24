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

    public void CreateOrder(string productName, int quantity)
    {
        var order = new Order
        {
            ProductName = productName,
            Quantity = quantity
        };

        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public List<Order> GetAllOrders()
    {
        return _context.Orders.ToList();
    }
}