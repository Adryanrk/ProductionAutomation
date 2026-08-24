using OrderSystem.Models;

namespace OrderSystem.Services;

public class OrderValidator
{
    public bool IsValid(Order order)
    {
        if (string.IsNullOrWhiteSpace(order.ProductName))
        {
            return false;
        }

        if (order.Quantity <= 0)
        {
            return false;
        }

        return true;
    }
}