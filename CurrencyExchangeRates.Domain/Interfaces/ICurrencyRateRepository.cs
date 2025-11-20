using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Domain.Enums;

namespace CurrencyExchangeRates.Domain.Interfaces;
public interface ICurrencyRateRepository
{
    Task<CurrencyTable?> GetLatestTableAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CurrencyTable>> GetCurrencyHistoryByCode(string code, CancellationToken cancellationToken);
    Task<bool> ExistsByEffectiveDateAsync(DateTime date, CancellationToken cancellationToken);
    Task AddAsync(CurrencyTable table, CancellationToken cancellationToken);
    Task<bool> AnyRatesAsync(CancellationToken cancellationToken);
}