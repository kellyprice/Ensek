using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MeterReadingDTO>().HasKey(table => new {
                table.AccountId,
                table.MeterReadingDateTime,
                table.MeterReadingValue
            });
        }

        public DbSet<AccountDTO>? Accounts { get; set; }
        public DbSet<MeterReadingDTO>? MeterReadings { get; set; }
    }
}
