using CurrencyExchangeRates.Application.Interfaces;
using CurrencyExchangeRates.Application.Models.NbpModels;
using System.Net.Http.Json;

namespace CurrencyExchangeRates.Infrastructure.Clients;

public class NbpClient : INbpClient
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://api.nbp.pl/api/")
    };

    public async Task<NbpTableBResponse?> GetTableAsync(CancellationToken cancellationToken)
    {
        var typeCode = "B";

        var response = await _httpClient.GetAsync($"exchangerates/tables/{typeCode}/?format=json", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var list = await response.Content.ReadFromJsonAsync<List<NbpTableBResponse>>(cancellationToken: cancellationToken);

        return list?.FirstOrDefault();
    }
}