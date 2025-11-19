using CurrencyExchangeRates.Application.Models;
using CurrencyExchangeRates.Application.Models.NbpModels;
using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Domain.Enums;

namespace CurrencyExchangeRates.Application.Mapping;
public static class CurrencyRateMapper
{
    public static CurrencyTableDto Map(CurrencyTable table)
    {
        return new CurrencyTableDto
        {
            TableNumber = table.TableNumber,
            Table = table.TableType.ToString(),
            EffectiveDate = table.EffectiveDate,
            Rates = table.Rates.Select(it => new CurrencyRateDto
            {
                Code = it.Code,
                Currency = it.Currency,
                Rate = it.Rate
            }).ToList()
        };
    }

    public static CurrencyHistoryDto Map(CurrencyTable table, CurrencyRate rate)
    {
        return new CurrencyHistoryDto
        {
            EffectiveDate = table.EffectiveDate,
            Rate = rate.Rate
        };
    }

    public static CurrencyTable Map(NbpTableBResponse nbpTableBResponse)
    {
        return new CurrencyTable
        {
            TableType = Enum.Parse<NbpTableType>(nbpTableBResponse.Table, true),
            TableNumber = nbpTableBResponse.No,
            EffectiveDate = nbpTableBResponse.EffectiveDate,

            Rates = nbpTableBResponse.Rates.Select(it => new CurrencyRate
            {
                Id = Guid.NewGuid(),
                Code = it.Code,
                Currency = it.Currency,
                Rate = it.Mid
            }).ToList()
        };
    }
}