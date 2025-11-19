using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CurrencyExchangeRates.Infrastructure;
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CurrencyTable> CurrencyTables { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyTable>().HasMany(it => it.Rates).WithOne().OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CurrencyTable>().Property(it => it.TableType).HasConversion(new EnumToStringConverter<NbpTableType>());
        modelBuilder.Entity<CurrencyRate>().Property(it => it.Code).IsRequired().HasMaxLength(5);
        modelBuilder.Entity<CurrencyRate>().Property(it => it.Currency).IsRequired();
        modelBuilder.Entity<CurrencyRate>().Property(it => it.Rate).HasColumnType("decimal(18,8)");
    }
}