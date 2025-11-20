namespace CurrencyExchangeRates.Domain.Entities;
public class CurrencyRate
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public Guid CurrencyTableId { get; set; }
    public CurrencyTable CurrencyTable { get; set; } = null!;


}