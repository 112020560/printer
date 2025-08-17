using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrinterService.Domain.Abstraction.Printer.Provider;
using PrinterService.Domain.Enums;
using PrinterService.Domain.Models;
using PrinterService.Domain.Settings;

namespace PrinterService.Domain.Services;

public class PrintService: IPrintService
{
    private readonly ITicketTemplateService _templateService;
    private readonly Func<string, IPrinterProvider> _printerProvider;
     private readonly ILogger<PrintService> _logger;
     private readonly PrinterSettings _settings;

    public PrintService(ITicketTemplateService ticketTemplateService, Func<string, IPrinterProvider> printerProvider, IOptions<PrinterSettings> settings, ILogger<PrintService> logger)
    {
        _templateService = ticketTemplateService;
        _printerProvider = printerProvider;
        _logger = logger;
        _settings = settings.Value;
    }
    public async Task<bool> PrintTicketAsync(PrintJob printJob)
    {
        try
        {
            var template = _templateService.GenerateTicketTemplate(printJob.TicketData, printJob.PrinterType);

            return await _printerProvider(printJob.PrinterName.ToString()).PrintToProviderAsync(template, printJob.PrinterName, _settings.IpAddress, _settings.Port);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error printing ticket {printJob.Id}", printJob.Id);
            return false;
        }
    }
}
