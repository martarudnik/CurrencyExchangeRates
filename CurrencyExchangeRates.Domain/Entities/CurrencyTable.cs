using CurrencyExchangeRates.Domain.Enums;

namespace CurrencyExchangeRates.Domain.Entities;
public class CurrencyTable
{
    public Guid Id { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public NbpTableType TableType { get; set; }
    public DateTime EffectiveDate { get; set; }
    public List<CurrencyRate> Rates { get; set; } = [];
}