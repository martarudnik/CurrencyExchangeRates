using CurrencyExchangeRates.Application.Interfaces;
using CurrencyExchangeRates.Application.Models.NbpModels;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace CurrencyExchangeRates.Infrastructure.Clients;

public class NbpClient(ILogger<NbpClient> logger) : INbpClient
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://api.nbp.pl/api/")
    };

    private readonly ILogger<NbpClient> _logger = logger;

    public async Task<NbpTableBResponse?> GetTableAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching NBP table B...");

            var response = await _httpClient.GetAsync("exchangerates/tables/B/?format=json",cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("NBP returned status code {StatusCode}.", response.StatusCode);

                return null;
            }

            var list = await response.Content.ReadFromJsonAsync<List<NbpTableBResponse>>(cancellationToken);

            var result = list?.FirstOrDefault();

            if (result == null)
            {
                _logger.LogWarning("NBP returned an empty list for table B.");
            }
            else
            {
                _logger.LogInformation("NBP returned {Count} currency rates.", result.Rates.Count);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling NBP API.");
            return null;
        }
    }
}