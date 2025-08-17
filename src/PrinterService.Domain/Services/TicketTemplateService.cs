using System;
using System.Text;
using PrinterService.Domain.Contracts;
using PrinterService.Domain.Enums;

namespace PrinterService.Domain.Services;

public class TicketTemplateService: ITicketTemplateService
{
    public string GenerateTicketTemplate(TicketData ticketData, PrinterType printerType)
    {
        return printerType switch
        {
            PrinterType.Thermal => GenerateThermalTicket(ticketData),
            _ => GenerateStandardTicket(ticketData)
        };
    }

    private string GenerateThermalTicket(TicketData ticketData)
    {
        var sb = new StringBuilder();

        // Header
        sb.AppendLine(CenterText(ticketData.BusinessName, 32));
        sb.AppendLine(CenterText(ticketData.BusinessAddress, 32));
        sb.AppendLine(CenterText(ticketData.BusinessPhone, 32));
        sb.AppendLine(new string('=', 32));

        // Ticket info
        sb.AppendLine($"Ticket: {ticketData.TicketNumber}");
        sb.AppendLine($"Fecha: {ticketData.Date:dd/MM/yyyy HH:mm}");
        if (!string.IsNullOrEmpty(ticketData.CustomerName))
        {
            sb.AppendLine($"Cliente: {ticketData.CustomerName}");
        }
        sb.AppendLine(new string('-', 32));

        // Items
        foreach (var item in ticketData.Items)
        {
            sb.AppendLine($"{item.Name}");
            sb.AppendLine($"{item.Quantity} x {item.UnitPrice:C} = {item.Total:C}");
        }

        sb.AppendLine(new string('-', 32));

        // Totals
        sb.AppendLine($"Subtotal: {ticketData.Subtotal:C}".PadLeft(32));
        sb.AppendLine($"IVA: {ticketData.Tax:C}".PadLeft(32));
        sb.AppendLine($"TOTAL: {ticketData.Total:C}".PadLeft(32));

        sb.AppendLine(new string('=', 32));
        sb.AppendLine($"Pago: {ticketData.PaymentMethod}");
        sb.AppendLine();
        sb.AppendLine(CenterText(ticketData.Footer, 32));

        return sb.ToString();
    }

    private string GenerateStandardTicket(TicketData ticketData)
    {
        var sb = new StringBuilder();

        // Header
        sb.AppendLine(CenterText(ticketData.BusinessName, 50));
        sb.AppendLine(CenterText(ticketData.BusinessAddress, 50));
        sb.AppendLine(CenterText(ticketData.BusinessPhone, 50));
        sb.AppendLine(new string('=', 50));

        // Ticket info
        sb.AppendLine($"Ticket No: {ticketData.TicketNumber}");
        sb.AppendLine($"Fecha: {ticketData.Date:dd/MM/yyyy HH:mm:ss}");
        if (!string.IsNullOrEmpty(ticketData.CustomerName))
        {
            sb.AppendLine($"Cliente: {ticketData.CustomerName}");
        }
        sb.AppendLine(new string('-', 50));

        // Items header
        sb.AppendLine("Producto".PadRight(25) + "Cant".PadRight(6) + "Precio".PadRight(10) + "Total".PadLeft(9));
        sb.AppendLine(new string('-', 50));

        // Items
        foreach (var item in ticketData.Items)
        {
            var name = item.Name.Length > 24 ? item.Name.Substring(0, 24) : item.Name;
            sb.AppendLine($"{name.PadRight(25)}{item.Quantity.ToString().PadRight(6)}{item.UnitPrice:C}".PadRight(41) + $"{item.Total:C}".PadLeft(9));
        }

        sb.AppendLine(new string('-', 50));

        // Totals
        sb.AppendLine($"Subtotal: {ticketData.Subtotal:C}".PadLeft(50));
        sb.AppendLine($"IVA: {ticketData.Tax:C}".PadLeft(50));
        sb.AppendLine($"TOTAL: {ticketData.Total:C}".PadLeft(50));

        sb.AppendLine(new string('=', 50));
        sb.AppendLine($"Método de pago: {ticketData.PaymentMethod}");
        sb.AppendLine();
        sb.AppendLine(CenterText(ticketData.Footer, 50));

        return sb.ToString();
    }

    private string CenterText(string text, int width)
    {
        if (text.Length >= width) return text;

        int padding = (width - text.Length) / 2;
        return text.PadLeft(text.Length + padding).PadRight(width);
    }
}

