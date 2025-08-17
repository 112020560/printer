using System;

namespace PrinterService.Domain.Abstraction.Printer.Provider;

public interface IPrinterProvider
{
   Task<bool> PrintToProviderAsync(string content, string? printerName = null, string? ipAddress = null, int? port = null);
}
