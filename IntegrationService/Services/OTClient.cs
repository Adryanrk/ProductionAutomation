using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using IntegrationService.Models;

namespace IntegrationService.Services;

public class OTClient
{
    private readonly string _host = "127.0.0.1";
    private readonly int _port = 5000;

    public bool SendOrder(IntegrationOrder order)
    {
        try
        {
            using TcpClient client = new TcpClient();

            client.Connect(_host, _port);

            using NetworkStream stream = client.GetStream();

            string json = JsonSerializer.Serialize(order);
            byte[] data = Encoding.UTF8.GetBytes(json);

            stream.Write(data, 0, data.Length);

            return true;
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Could not connect to OT system: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending order: {ex.Message}");
            return false;
        }
    }
}