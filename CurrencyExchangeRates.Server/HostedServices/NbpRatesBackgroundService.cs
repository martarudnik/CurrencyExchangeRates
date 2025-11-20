using CurrencyExchangeRates.Application.Interfaces;

namespace CurrencyExchangeRates.Server.HostedServices;

public class NbpRatesScheduler(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private const int MaxRetries = 3;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var currencyRateService = scope.ServiceProvider.GetRequiredService<ICurrencyRateService>();

                bool hasAnyRates = await currencyRateService.AnyRatesAsync(cancellationToken);

                if (!hasAnyRates)
                {
                    await RunWithRetry(cancellationToken);
                }
            }

#if DEBUG
            await RunWithRetry(cancellationToken);
#else
            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = GetDelayUntilNextRun(12, 30);

                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    // normalne gdy aplikacja się wyłącza
                    return;
                }

                await RunWithRetry(stoppingToken);
            }
#endif
        }
        catch (Exception ex)
        {
            Console.WriteLine("Scheduler error: " + ex);
        }
    }

    private async Task RunWithRetry(CancellationToken cancellationToken)
    {
        int attempt = 0;

        while (attempt < MaxRetries && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var currencyRateService = scope.ServiceProvider.GetRequiredService<ICurrencyRateService>();
                await currencyRateService.SyncLatestRatesAsync(cancellationToken);

                return;
            }
            catch
            {
                attempt++;

                if (attempt >= MaxRetries)
                    return;

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }
    }

    private static TimeSpan GetDelayUntilNextRun(int hour, int minute)
    {
        var now = DateTime.Now;
        var next = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);

        if (next <= now)
            next = next.AddDays(1);

        return next - now;
    }
}
