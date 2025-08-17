using Microsoft.Extensions.DependencyInjection;
using PrinterService.Domain.Services;

namespace PrinterService.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    => services.AddDomainServices();
    
    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ITicketTemplateService, TicketTemplateService>();
        services.AddScoped<IPrintService, PrintService>();

        return services;
    }
}
