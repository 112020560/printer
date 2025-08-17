using System;
using PrinterService.Domain.Enums;

namespace PrinterService.Domain.Settings;

public class PrinterSettings
{
    public string DefaultPrinterName { get; set; } = "";
    public PrinterType Type { get; set; } = PrinterType.Thermal;
    public string IpAddress { get; set; } = "";
    public int Port { get; set; } = 9100;
}
