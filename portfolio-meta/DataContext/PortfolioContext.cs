using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace DataContext
{
    public class PortfolioContext(DbContextOptions<PortfolioContext> options) : DbContext(options)
    {
        public async Task<bool> TestConnectionAsync()
        {
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
