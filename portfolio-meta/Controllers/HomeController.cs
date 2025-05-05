using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio.DataContext;
using portfolio.Models;
using System.Diagnostics;

namespace portfolio.Controllers
{
    public class HomeController(PortfolioContext context) : Controller
    {
        private readonly PortfolioContext _context = context;

        public async Task<IActionResult> Index()
        {
            var entries = await _context.Entries
                .Select(e => {
                    e.Tags = _context.Entries
                        .Join(
                            _context.Tags,
                            entry => entry.ID,
                            tag => tag.ID,
                            (e, tag) => tag)
                        .ToList();
                    return e;
                    })
                .OrderBy(e => e.Created)
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
