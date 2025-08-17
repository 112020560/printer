using System;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace PrinterService.Infrastructure.Printers.Providers;

public abstract class CommonProvidersFunctions
{
    protected string ConvertToEscPos(string content)
    {
        var sb = new StringBuilder();

        // Comandos ESC/POS básicos
        sb.Append("\x1B\x40"); // Inicializar impresora
        sb.Append("\x1B\x61\x01"); // Centrar texto

        sb.Append(content);

        sb.Append("\n\n\n");
        sb.Append("\x1D\x56\x00"); // Cortar papel

        return sb.ToString();
    }

    protected async Task<bool> SendToPrinterAsync(string content, string printerName)
    {
        return await Task.Run(() =>
        {
            try
            {
                var printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                printDocument.PrintPage += (sender, e) =>
                {
                    if (e.Graphics != null)
                    {
                        var font = new Font("Courier New", 8);
                        e.Graphics.DrawString(content, font, Brushes.Black, 0, 0);
                    }
                };

                printDocument.Print();
                return true;
            }
            catch
            {
                return false;
            }
        });
    }
}
