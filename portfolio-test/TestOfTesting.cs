using DataContext;
using Microsoft.EntityFrameworkCore;

namespace portfolio_test
{
    // Simple test to ensure that the testing harness works.
    [TestClass]
    public sealed class TestOfTesting
    {
        private static DbContextOptions<PortfolioContext> _options = new DbContextOptionsBuilder<PortfolioContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestMethod]
        public void TestDatabaseWorks()
        {
            using var ctx = new PortfolioContext(_options);
            Assert.IsTrue(ctx.Database.EnsureCreated());
        }
    }
}
