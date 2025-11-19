namespace CurrencyExchangeRates.Application.Models;
public class CurrencyHistoryDto
{
    public DateTime EffectiveDate { get; set; }
    public decimal Rate { get; set; }
}