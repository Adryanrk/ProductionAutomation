using OTSystem.Services;
using OTSystem.UI;

var productionService = new ProductionService();
var menu = new OTMenu(productionService);

menu.Run();