using System;
using PrinterService.Domain.Models;

namespace PrinterService.Domain.Services;

public interface IPrintService
{
    Task<bool> PrintTicketAsync(PrintJob printJob);
}
