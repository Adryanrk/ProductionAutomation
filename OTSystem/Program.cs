using OTSystem.Services;

var productionService = new ProductionService();
var server = new OTServer(productionService);

server.Start();