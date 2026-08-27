using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using IntegrationService.Models;

namespace IntegrationService.Services;

public class OTClient
{
    private readonly string _host = "127.0.0.1";
    private readonly int _port = 5000;

    public string? SendOrder(IntegrationOrder order)
    {
        try
        {
            using TcpClient client = new TcpClient();

            client.Connect(_host, _port);

            using NetworkStream stream = client.GetStream();

            string json = JsonSerializer.Serialize(order);
            byte[] data = Encoding.UTF8.GetBytes(json);

            // Send order to OT system
            stream.Write(data, 0, data.Length);

            // Wait for response from OT system
            byte[] responseBuffer = new byte[1024];
            int bytesRead = stream.Read(
                responseBuffer,
                0,
                responseBuffer.Length);

            string response = Encoding.UTF8.GetString(
                responseBuffer,
                0,
                bytesRead);

            return response;
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Could not connect to OT system: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending order: {ex.Message}");
            return null;
        }
    }
}