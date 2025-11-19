using CurrencyExchangeRates.Application.Models.NbpModels;

namespace CurrencyExchangeRates.Application.Interfaces;
public interface INbpClient
{
    Task<NbpTableBResponse?> GetTableAsync(CancellationToken cancellationToken);
}