namespace CurrencyExchangeRates.Application.Models.NbpModels;
public class NbpTableBResponse
{
    public string Table { get; set; } = string.Empty;
    public string No { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public List<NbpRateResponse> Rates { get; set; } = [];
}