using CurrencyExchangeRates.Application.Interfaces;
using CurrencyExchangeRates.Domain.Interfaces;
using CurrencyExchangeRates.Infrastructure.Clients;
using CurrencyExchangeRates.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchangeRates.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(it => it.UseSqlServer(connectionString));

        services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
		services.AddScoped<INbpClient, NbpClient>();

		return services;
    }
}