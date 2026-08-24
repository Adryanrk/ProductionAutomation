using OrderSystem.Data;
using OrderSystem.Services;
using OrderSystem.UI;

using var context = new AppDbContext();

context.Database.EnsureCreated();

var orderService = new OrderService(context);
var orderMenu = new OrderMenu(orderService);

orderMenu.Run();