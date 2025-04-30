using portfolio.DataContext;
using Microsoft.EntityFrameworkCore;
using portfolio.Models;

namespace portfolio_test;

[TestClass]
public class TestEntryTag
{
    [TestMethod]
    public void TestLinks()
    {
        using var ctx = new PortfolioContext(Config.DbOptions);

        // Create 2 tags
        var tag1 = ctx.Tags.Add(new Tag { Title = "DevOps", Description = "" });
        var tag2 = ctx.Tags.Add(new Tag { Title = "Performance", Description = "" });
        ctx.SaveChanges();

        // Create 1 entry
        var entryTracker = ctx.Entries.Add(new Entry
        {
            Title = "devops",
            Extract = "eh",
            Content = "...stuff about devops..."
        });
        ctx.SaveChanges();

        // Link 2 tags to 1 entry
        ctx.EntryTags.Add(new EntryTag {
            EntryID = entryTracker.Entity.ID,
            TagID = tag1.Entity.ID
        });
        ctx.EntryTags.Add(new EntryTag {
            EntryID = entryTracker.Entity.ID,
            TagID = tag2.Entity.ID
        });
        ctx.SaveChanges();

        // Verify tags
        var entryTags = ctx.EntryTags
            .Where(e => e.EntryID == entryTracker.Entity.ID)
            .Select(e => e.TagID)
            .ToList();
        Assert.AreEqual(2, entryTags.Count);
    }
}
