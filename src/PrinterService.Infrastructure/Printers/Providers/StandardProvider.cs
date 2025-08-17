using System;
using PrinterService.Domain.Abstraction.Printer.Provider;

namespace PrinterService.Infrastructure.Printers.Providers;

public class StandardProvider : CommonProvidersFunctions, IPrinterProvider
{
    public async Task<bool> PrintToProviderAsync(string content, string? printerName = null, string? ipAddress = null, int? port = null)
    {
        if (string.IsNullOrEmpty(printerName)) throw new ArgumentNullException(nameof(printerName));
        return await SendToPrinterAsync(content, printerName);
    }
}
