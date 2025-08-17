using System;
using PrinterService.Domain.Contracts;
using PrinterService.Domain.Enums;

namespace PrinterService.Domain.Models;

public class PrintJob
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PrinterName { get; set; } = "";
    public PrinterType PrinterType { get; set; }
    public TicketData TicketData { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
