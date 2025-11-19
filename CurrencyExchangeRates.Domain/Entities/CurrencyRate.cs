using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeRates.Domain.Entities;
public class CurrencyRate
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
}