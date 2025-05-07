using Microsoft.EntityFrameworkCore;
using portfolio.Models;
using portfolio.Models.ViewModels;

namespace portfolio.DataContext
{
    public class PortfolioContext(DbContextOptions<PortfolioContext> options) : DbContext(options)
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique titles for tags
            modelBuilder.Entity<Tag>()
                .HasIndex(tag => tag.Title)
                .IsUnique();

            // Unique titles for entries
            modelBuilder.Entity<Entry>()
                .HasIndex(entry => entry.Title)
                .IsUnique();

            // Enforce UTC for DateTime on 'Created' and 'LastUpdated'
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

        public async Task<EntryWithTagsViewModel> GetEntryWithTagsAsync(int entryId)
        {
            var entry = await Entries.FindAsync(entryId);
            if (entry == null) return null;

            var tags = await Tags
                .Where(tag => entry.Tags.Contains(tag.ID))
                .ToListAsync();

            return new EntryWithTagsViewModel
            {
                Entry = entry,
                Tags = tags
            };
        }
    }
}
