using DataContext;

namespace portfolio_test
{
    // Simple test to ensure that the testing harness works.
    [TestClass]
    public sealed class TestOfTesting
    {
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
            using var ctx = new PortfolioContext(Config.DbOptions);
            Assert.IsTrue(ctx.Database.CanConnect());
        }
    }
}
