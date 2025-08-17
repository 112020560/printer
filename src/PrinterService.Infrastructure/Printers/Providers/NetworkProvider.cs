using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;
using PrinterService.Domain.Abstraction.Printer.Provider;

namespace PrinterService.Infrastructure.Printers.Providers;

public class NetworkProvider : IPrinterProvider
{
    private readonly ILogger<NetworkProvider> _logger;
    public NetworkProvider(ILogger<NetworkProvider> logger)
    {
        _logger = logger;
    }
    public async Task<bool> PrintToProviderAsync(string content, string? printerName = null, string? ipAddress = null, int? port = null)
    {
        try
        {
            if (string.IsNullOrEmpty(ipAddress)) throw new ArgumentNullException(nameof(ipAddress));

            using var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ipAddress, port ?? 0);

            using var stream = tcpClient.GetStream();
            var data = Encoding.UTF8.GetBytes(content);
            await stream.WriteAsync(data, 0, data.Length);

            _logger.LogInformation("Sent to network printer {ipAddress}:{port}", ipAddress, port);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error printing to network printer {ipAddress}:{port}", ipAddress, port);
            return false;
        }
    }
}
