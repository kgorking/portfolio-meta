using portfolio.DataContext;
using portfolio.Models;

namespace portfolio_test;

[TestClass]
public class TestDbEntry
{
    [TestMethod]
    public void TestCRUD()
    {
        using var ctx = new PortfolioContext(Config.DbOptions);
        var entry = new Entry
        {
            Title = "title test",
            Content = "123456abcdef",
            Created = DateTime.Now,
            LastUpdated = DateTime.Now
        };

        // C
        ctx.Entries.Add(entry);
        Assert.AreEqual(1, ctx.SaveChanges());

        // R
        var readEntry = ctx.Entries.Where(e => e.ID == entry.ID).Single();
        Assert.AreEqual(entry, readEntry);

        // U
        ctx.Entries.Update(readEntry);
        readEntry.Content = "[redacted]";
        Assert.AreEqual(1, ctx.SaveChanges());

        var updatedEntry = ctx.Entries.Where(e => e.ID == entry.ID).Single();
        Assert.AreEqual(readEntry.Content, updatedEntry.Content);

        // D
        ctx.Entries.Remove(entry);
        Assert.AreEqual(1, ctx.SaveChanges());
    }

    [TestMethod]
    public void TestRequirements()
    {
        using var ctx = new PortfolioContext(Config.DbOptions);
        var entry = new Entry
        {
            Title = null
        };

        ctx.Entries.Add(entry);
        Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => ctx.SaveChanges());
    }
}
