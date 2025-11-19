using CurrencyExchangeRates.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeRates.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrenciesController(ICurrencyRateService currencyRateService) : ControllerBase
{
    private readonly ICurrencyRateService _currencyRateService = currencyRateService;

    /// <summary>
    /// Retrieves the latest currency exchange rate table.
    /// </summary>
    /// <remarks>
    /// Returns a full list of the most recent exchange rates.
    /// </remarks>
    /// <response code="200">Returns the latest currency table</response>
    /// <response code="404">No data found</response>
    [HttpGet]
    public async Task<IActionResult> GetLatest(CancellationToken cancellationToken)
    {
        var result = await _currencyRateService.GetLatestTableAsync(cancellationToken);

        if (result == null)
        {
            return NotFound("No data found.");
        }

        return Ok(result);
    }

    /// <summary>
    /// Retrieves historical exchange rate data for a given currency.
    /// </summary>
    /// <param name="code">The currency code, e.g., USD, EUR</param>
    /// <response code="200">Returns the currency's historical exchange rate data</response>
    /// <response code="404">No historical data found for the given currency</response>
    [HttpGet("{code}/history")]
    public async Task<IActionResult> GetHistory(string code, CancellationToken cancellationToken)
    {
        var history = await _currencyRateService.GetCurrencyHistoryByCode(code, cancellationToken);

        if (history == null || !history.Any())
        {
            return NotFound($"No historical data found for the given currency {code}.");
        }

        return Ok(history);
    }
}