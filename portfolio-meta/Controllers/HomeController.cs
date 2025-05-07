using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio.DataContext;
using portfolio.Models;
using portfolio.Models.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace portfolio.Controllers
{
    public class HomeController(PortfolioContext context) : Controller
    {
        private readonly PortfolioContext _context = context;

        public async Task<IActionResult> Index()
        {
            var entries = await _context.Entries
                .OrderBy(entry => entry.Created)
                .Select(entry =>
                    new EntryWithTagsViewModel
                    {
                        Entry = entry,
                        Tags = _context.Tags
                            .Where(tag => entry.Tags.Contains(tag.ID))
                            .ToList()
                    })
                .ToListAsync();
            return View(entries);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? message = null)
        {
            ViewData["Message"] = message;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
