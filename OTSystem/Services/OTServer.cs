using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using OTSystem.Models;

namespace OTSystem.Services;

public class OTServer
{
    private readonly ProductionService _productionService;
    private readonly int _port = 5000;

    public OTServer(ProductionService productionService)
    {
        _productionService = productionService;
    }

    public void Start()
    {
        TcpListener? listener = null;

        try
        {
            listener = new TcpListener(IPAddress.Loopback, _port);
            listener.Start();

            Console.WriteLine("============================");
            Console.WriteLine("       OT SYSTEM");
            Console.WriteLine("============================");
            Console.WriteLine();
            Console.WriteLine($"Waiting for orders on port {_port}...");

            while (true)
            {
                using TcpClient client = listener.AcceptTcpClient();
                using NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[4096];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                ProductionOrder? order =
                    JsonSerializer.Deserialize<ProductionOrder>(json);

                if (order == null)
                {
                    Console.WriteLine("Invalid order received.");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Order received from IntegrationService.");

                _productionService.StartProduction(order);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"OT server error: {ex.Message}");
        }
        finally
        {
            listener?.Stop();
        }
    }
}