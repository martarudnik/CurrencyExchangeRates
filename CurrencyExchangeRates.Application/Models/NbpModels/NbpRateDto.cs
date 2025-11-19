namespace CurrencyExchangeRates.Application.Models.NbpModels;
public class NbpRateDto
{
    public string Currency { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal Mid { get; set; }
}