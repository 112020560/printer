using System;

namespace PrinterService.Domain.Contracts;

public class TicketData
{
    public string BusinessName { get; set; } = "";
    public string BusinessAddress { get; set; } = "";
    public string BusinessPhone { get; set; } = "";
    public string TicketNumber { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
    public string CustomerName { get; set; } = "";
    public List<TicketItem> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; } = "";
    public string Footer { get; set; } = "¡Gracias por su compra!";
}

public class TicketItem
{
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;
}