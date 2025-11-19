namespace CurrencyExchangeRates.Application.Models;
public class CurrencyTableDto
{
    public string Table { get; set; } = string.Empty;
	public string TableNumber { get; set; } = string.Empty;
	public DateTime EffectiveDate { get; set; }
	public List<CurrencyRateDto> Rates { get; set; } = [];
}