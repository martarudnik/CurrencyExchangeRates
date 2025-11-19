using CurrencyExchangeRates.Application.Interfaces;
using CurrencyExchangeRates.Application.Models.NbpModels;
using CurrencyExchangeRates.Application.Services;
using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Domain.Interfaces;
using Moq;

namespace CurrencyExchangeRates.Tests.Application;

public class CurrencyRateServiceTests
{
    private readonly Mock<INbpClient> _nbpClient = new();
    private readonly Mock<ICurrencyRateRepository> _currencyRateRepository = new();

    [Fact]
    public async Task SyncLatestRatesAsync_SavesRates_WhenNbpReturnsData()
    {
        // Arrange
        _nbpClient.Setup(x => x.GetTableAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new NbpTableBResponse
                  {
                      Table = "B",
                      EffectiveDate = DateTime.Now,
                      Rates =
                      [
                          new() { Code = "USD", Currency = "dolar", Mid = 4.02m },
                          new() { Code = "GEL", Currency = "lari", Mid = 1.3546m }
                      ]
                  });

        var service = new CurrencyRateService(_currencyRateRepository.Object, _nbpClient.Object);

        // Act
        await service.SyncLatestRatesAsync(CancellationToken.None);

        // Assert
        _currencyRateRepository.Verify(x => x.AddAsync(
               It.Is<CurrencyTable>(t =>
                   t.Rates.Count == 2 &&
                   t.Rates.Any(r => r.Code == "USD") &&
                   t.Rates.Any(r => r.Code == "GEL")
               ),
               It.IsAny<CancellationToken>()),
           Times.Once);
    }

    [Fact]
    public async Task SyncLatestRatesAsync_DoesNotSave_WhenNbpReturnsNull()
    {
        // Arrange
        _nbpClient.Setup(x => x.GetTableAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync((NbpTableBResponse?)null);

        var service = new CurrencyRateService(_currencyRateRepository.Object, _nbpClient.Object);

        // Act
        await service.SyncLatestRatesAsync(CancellationToken.None);

        // Assert
        _currencyRateRepository.Verify(it => it.AddAsync(It.IsAny<CurrencyTable>(),
                                                         It.IsAny<CancellationToken>()),
                                                         Times.Never);
    }

    [Fact]
    public async Task SyncLatestRatesAsync_DoesNotSave_WhenNbpReturnsEmptyRates()
    {
        // Arrange
        _nbpClient.Setup(x => x.GetTableAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new NbpTableBResponse
                  {
                      Table = "B",
                      EffectiveDate = DateTime.Now,
                      Rates = []
                  });

        var service = new CurrencyRateService(_currencyRateRepository.Object, _nbpClient.Object);

        // Act
        await service.SyncLatestRatesAsync(CancellationToken.None);

        // Assert
        _currencyRateRepository.Verify(it => it.AddAsync(It.IsAny<CurrencyTable>(),
                                                         It.IsAny<CancellationToken>()),
                                                         Times.Never);
    }
}