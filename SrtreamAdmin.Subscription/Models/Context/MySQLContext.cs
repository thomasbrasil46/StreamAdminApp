using Microsoft.EntityFrameworkCore;

namespace StreamAdmin.Subscription.Models.Context;

public class MySQLContext : DbContext
{
    public MySQLContext() { }
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var subscription = modelBuilder.Entity<UserSubscription>();

        subscription.OwnsOne(item => item.Price, money =>
        {
            money.Property(item => item.Amount)
                .HasColumnName("sus_price")
                .HasPrecision(18, 2)
                .IsRequired();
            money.Property(item => item.Currency)
                .HasColumnName("sus_currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        subscription.Property(item => item.BillingCycle).HasConversion<string>();
        subscription.Property(item => item.Status).HasConversion<string>();
    }
}