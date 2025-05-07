using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio.DataContext;
using portfolio.Models;

namespace portfolio.Controllers
{
    public class EntriesController(PortfolioContext context) : Controller
    {
        private readonly PortfolioContext _context = context;

        // GET: Entries
        public async Task<IActionResult> Index()
        {
            var entries = await _context.Entries.ToListAsync();
            return View(entries);
        }

        // GET: Entries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: Entries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Url,Tags")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                entry.Created = DateTime.UtcNow;
                entry.LastUpdated = DateTime.UtcNow;
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entry);
        }

        // GET: Entries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Extract,Content")] Entry entry)
        {
            if (id != entry.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //entry.LastUpdated = DateTime.UtcNow;
                    //_context.Entries.Update(entry);
                    //await _context.SaveChangesAsync();
                    await _context.Entries
                        .Where(e => e.ID == entry.ID)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(e => e.Title, e => entry.Title)
                            .SetProperty(e => e.Content, e => entry.Content)
                            .SetProperty(e => e.LastUpdated, e => DateTime.UtcNow)
                    );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryExists(entry.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(entry);
        }

        // GET: Entries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _context.Entries.FindAsync(id);
            if (entry != null)
            {
                _context.Entries.Remove(entry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.ID == id);
        }
    }
}
