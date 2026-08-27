using OrderSystem.Models;
using OrderSystem.Services;

namespace ProductionAutomation.Tests;

public class OrderValidatorTests
{
    [Fact]
    public void IsValid_ValidOrder_ReturnsTrue()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            ProductName = "Steel Plate",
            Quantity = 10
        };

        bool result = validator.IsValid(order);

        Assert.True(result);
    }

    [Fact]
    public void IsValid_EmptyProductName_ReturnsFalse()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            ProductName = "",
            Quantity = 10
        };

        bool result = validator.IsValid(order);

        Assert.False(result);
    }

    [Fact]
    public void IsValid_WhitespaceProductName_ReturnsFalse()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            ProductName = "   ",
            Quantity = 10
        };

        bool result = validator.IsValid(order);

        Assert.False(result);
    }

    [Fact]
    public void IsValid_NumericProductName_ReturnsFalse()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            ProductName = "12345",
            Quantity = 10
        };

        bool result = validator.IsValid(order);

        Assert.False(result);
    }

    [Fact]
    public void IsValid_ZeroQuantity_ReturnsFalse()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            ProductName = "Gear",
            Quantity = 0
        };

        bool result = validator.IsValid(order);

        Assert.False(result);
    }

    [Fact]
    public void IsValid_NegativeQuantity_ReturnsFalse()
    {
        var validator = new OrderValidator();

        var order = new Order
        {
            ProductName = "Gear",
            Quantity = -5
        };

        bool result = validator.IsValid(order);

        Assert.False(result);
    }
}