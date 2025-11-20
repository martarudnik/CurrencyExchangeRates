using CurrencyExchangeRates.Application.Models;

namespace CurrencyExchangeRates.Application.Interfaces;
public interface ICurrencyRateService
{
    Task<CurrencyTableDto?> GetLatestTableAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CurrencyHistoryDto>> GetCurrencyHistoryByCode(string code, CancellationToken cancellationToken);
    Task SyncLatestRatesAsync(CancellationToken cancellationToken);
    Task<bool> AnyRatesAsync(CancellationToken cancellationToken);
}