using CurrencyExchangeRates.Application.Interfaces;
using CurrencyExchangeRates.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchangeRates.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyRateService, CurrencyRateService>();

        return services;
    }
}