using CurrencyExchangeRates.Application.Interfaces;
using CurrencyExchangeRates.Application.Mapping;
using CurrencyExchangeRates.Application.Models;
using CurrencyExchangeRates.Domain.Interfaces;

namespace CurrencyExchangeRates.Application.Services;
public class CurrencyRateService(ICurrencyRateRepository currencyRateRepository, INbpClient nbpClient) : ICurrencyRateService
{
    private readonly ICurrencyRateRepository _currencyRateRepository = currencyRateRepository;
    private readonly INbpClient _nbpClient = nbpClient;

    public async Task<CurrencyTableDto?> GetLatestTableAsync(CancellationToken cancellationToken)
    {
        var latestTable = await _currencyRateRepository.GetLatestTableAsync(cancellationToken);
        if (latestTable == null)
        {
            return null;
        }

        return CurrencyRateMapper.Map(latestTable);
    }

    public async Task<IEnumerable<CurrencyHistoryDto>> GetCurrencyHistoryByCode(string code,CancellationToken cancellationToken)
    {
        var tables = await _currencyRateRepository.GetCurrencyHistoryByCode(code, cancellationToken);

        return tables.Select(it => (it, rate: it.Rates?.FirstOrDefault()))
                     .Where(it => it.rate != null)
                     .Select(it => CurrencyRateMapper.Map(it.it, it.rate!));
    }

    public async Task SyncLatestRatesAsync(CancellationToken cancellationToken)
    {
        var table = await _nbpClient.GetTableAsync(cancellationToken);

        if (table == null || table.Rates == null || table.Rates.Count == 0)
        {
            return;
        }

        var exists = await _currencyRateRepository.ExistsByEffectiveDateAsync(table.EffectiveDate, cancellationToken);
        if (exists)
        {
            return;
        }

        var entity = CurrencyRateMapper.Map(table);

        await _currencyRateRepository.AddAsync(entity, cancellationToken);
    }

    public Task<bool> AnyRatesAsync(CancellationToken cancellationToken)
    {
        return _currencyRateRepository.AnyRatesAsync(cancellationToken);
    }
}