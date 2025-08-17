using System;
using Microsoft.Extensions.Logging;
using PrinterService.Domain.Abstraction.Printer.Provider;

namespace PrinterService.Infrastructure.Printers.Providers;

public class ThermalProvider : CommonProvidersFunctions, IPrinterProvider
{
    private readonly ILogger<ThermalProvider> _logger;
    public ThermalProvider(ILogger<ThermalProvider> logger)
    {
        _logger = logger;
    }
    public async Task<bool> PrintToProviderAsync(string content, string? printerName = null, string? ipAddress = null, int? port = null)
    {
        try
        {
            if (string.IsNullOrEmpty(printerName))
                throw new Exception("The printerName is required.");
            // Para impresoras térmicas, usamos comandos ESC/POS
            var escPosContent = ConvertToEscPos(content);
            return await SendToPrinterAsync(escPosContent, printerName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error printing to thermal printer");
            return false;
        }
    }
}
