using System;
using PrinterService.Domain.Contracts;
using PrinterService.Domain.Enums;

namespace PrinterService.Domain.Services;

public interface ITicketTemplateService
{
    string GenerateTicketTemplate(TicketData ticketData, PrinterType printerType);
}
