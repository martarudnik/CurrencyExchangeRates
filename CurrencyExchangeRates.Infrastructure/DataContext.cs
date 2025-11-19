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
        modelBuilder.Entity<CurrencyTable>().Property(it => it.TableType)
                                            .HasConversion(new EnumToStringConverter<NbpTableType>());
        modelBuilder.Entity<CurrencyTable>().HasIndex(t => t.EffectiveDate);
        modelBuilder.Entity<CurrencyTable>().HasIndex(t => t.TableType);
        modelBuilder.Entity<CurrencyRate>().HasOne(r => r.CurrencyTable)
                                           .WithMany(t => t.Rates)
                                           .HasForeignKey(r => r.CurrencyTableId)
                                           .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CurrencyRate>().Property(it => it.Code)
                                           .IsRequired()
                                           .HasMaxLength(5);
        modelBuilder.Entity<CurrencyRate>().Property(it => it.Currency)
                                           .IsRequired();
        modelBuilder.Entity<CurrencyRate>().Property(it => it.Rate)
                                           .HasColumnType("decimal(18,8)");
        modelBuilder.Entity<CurrencyRate>().HasIndex(r => r.Code);
        modelBuilder.Entity<CurrencyRate>().HasIndex(r => r.CurrencyTableId);
    }
}