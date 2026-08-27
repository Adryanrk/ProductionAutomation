using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Models;
using OrderSystem.Services;

namespace ProductionAutomation.Tests;

public class OrderServiceIntegrationTests
{
    private AppDbContext CreateTestContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        var context = new AppDbContext(options);

        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }

    [Fact]
    public void CreateOrder_ValidOrder_SavesOrderToDatabase()
    {
        using var context = CreateTestContext();

        var service = new OrderService(context);

        bool result = service.CreateOrder("Gear", 10);

        var orders = context.Orders.ToList();

        Assert.True(result);
        Assert.Single(orders);
        Assert.Equal("Gear", orders[0].ProductName);
        Assert.Equal(10, orders[0].Quantity);
        Assert.Equal("Pending", orders[0].Status);
    }

    [Fact]
    public void CreateOrder_InvalidOrder_DoesNotSaveToDatabase()
    {
        using var context = CreateTestContext();

        var service = new OrderService(context);

        bool result = service.CreateOrder("", 10);

        var orders = context.Orders.ToList();

        Assert.False(result);
        Assert.Empty(orders);
    }

    [Fact]
    public void GetAllOrders_ReturnsSavedOrders()
    {
        using var context = CreateTestContext();

        context.Orders.Add(new Order
        {
            ProductName = "Motor Housing",
            Quantity = 25
        });

        context.SaveChanges();

        var service = new OrderService(context);

        var orders = service.GetAllOrders();

        Assert.Single(orders);
        Assert.Equal("Motor Housing", orders[0].ProductName);
        Assert.Equal(25, orders[0].Quantity);
    }
}