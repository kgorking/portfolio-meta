using DataContext;
using Microsoft.EntityFrameworkCore;

namespace portfolio_test
{
    [TestClass]
    public sealed class Test1
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
        public void TestMethod1()
        {
            using var ctx = new PortfolioContext(_options);
            Assert.IsTrue(ctx.Database.EnsureCreated());
        }
    }
}
