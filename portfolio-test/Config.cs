using portfolio.DataContext;
using Microsoft.EntityFrameworkCore;

namespace portfolio_test
{
    internal class Config
    {
        public static DbContextOptions<PortfolioContext> DbOptions = new DbContextOptionsBuilder<PortfolioContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
    }
}
