using Microsoft.EntityFrameworkCore;
using portfolio.Models;

namespace portfolio.DataContext
{
    public class PortfolioContext(DbContextOptions<PortfolioContext> options) : DbContext(options)
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<EntryTag> EntryTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure that only one EntryTag with a specific tag/entry id combination exists
            modelBuilder.Entity<EntryTag>()
                .HasIndex(et => new { et.EntryID, et.TagID })
                .IsUnique();

            // Unique titles for tags
            modelBuilder.Entity<Tag>()
                .HasIndex(tag => tag.Title)
                .IsUnique();

            // Unique titles for entries
            modelBuilder.Entity<Entry>()
                .HasIndex(entry => entry.Title)
                .IsUnique();

            // Enforce UTC for DateTime
            modelBuilder.Entity<Entry>()
                .Property(e => e.Created)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            modelBuilder.Entity<Entry>()
                .Property(e => e.LastUpdated)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );
        }

        // Make sure that a connection to the database can be established
        public async Task<bool> TestConnectionAsync()
        {
            await Database.EnsureCreatedAsync();

            if (Database.IsInMemory())
                return true;

            try
            {
                await this.Database.OpenConnectionAsync();
                await this.Database.CloseConnectionAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}
