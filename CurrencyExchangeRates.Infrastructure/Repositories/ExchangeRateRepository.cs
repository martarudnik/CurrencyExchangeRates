using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRates.Infrastructure.Repositories;

public class CurrencyRateRepository(DataContext context) : ICurrencyRateRepository
{
    private readonly DataContext _context = context;

    public async Task<CurrencyTable?> GetLatestTableAsync(CancellationToken cancellationToken)
    {
        return await _context.CurrencyTables.Include(it => it.Rates)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<CurrencyTable>> GetCurrencyHistoryByCode(string code, CancellationToken cancellationToken)
    {
        return await _context.CurrencyTables.Where(it => it.Rates.Any(x => x.Code.Equals(code.ToUpper())))
                                            .Include(it => it.Rates)
                                            .AsNoTracking()
                                            .OrderByDescending(x => x.EffectiveDate)
                                            .ToListAsync(cancellationToken);
    }
    public async Task<bool> ExistsByEffectiveDateAsync(DateTime date, CancellationToken cancellationToken)
    {
        return await _context.CurrencyTables.AnyAsync(it => it.EffectiveDate == date, cancellationToken);
    }

    public async Task AddAsync(CurrencyTable table, CancellationToken cancellationToken)
    {

        await _context.CurrencyTables.AddAsync(table, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}