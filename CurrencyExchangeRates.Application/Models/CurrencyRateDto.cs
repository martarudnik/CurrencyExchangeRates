namespace CurrencyExchangeRates.Application.Models;
public class CurrencyRateDto
{
    public string Currency { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal Rate { get; set; }
}