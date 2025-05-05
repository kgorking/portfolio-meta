using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio.DataContext;
using portfolio.Models;

namespace portfolio.Controllers
{
    public class EntryTagsController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly EntriesController _entries;
        private readonly TagsController _tags;

        public EntryTagsController(PortfolioContext context)
        {
            _context = context;
            _entries = new EntriesController(_context);
            _tags = new TagsController(_context);
        }

        // GET: EntryTags
        public async Task<IActionResult> Index()
        {
            return View(await _context.EntryTags.ToListAsync());
        }

        // GET: EntryTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entryTag = await _context.EntryTags
                .FirstOrDefaultAsync(m => m.ID == id);
            if (entryTag == null)
            {
                return NotFound();
            }

            return View(entryTag);
        }

        // GET: EntryTags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EntryTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryID,TagID")] EntryTag entryTag)
        {
            if (ModelState.IsValid && _entries.EntryExists(entryTag.EntryID) && _tags.TagExists(entryTag.TagID))
            {
                _context.Add(entryTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entryTag);
        }

        // GET: EntryTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entryTag = await _context.EntryTags.FindAsync(id);
            if (entryTag == null)
            {
                return NotFound();
            }
            return View(entryTag);
        }

        // POST: EntryTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EntryID,TagID")] EntryTag entryTag)
        {
            if (id != entryTag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entryTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryTagExists(entryTag.ID))
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
            return View(entryTag);
        }

        // GET: EntryTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entryTag = await _context.EntryTags
                .FirstOrDefaultAsync(m => m.ID == id);
            if (entryTag == null)
            {
                return NotFound();
            }

            return View(entryTag);
        }

        // POST: EntryTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entryTag = await _context.EntryTags.FindAsync(id);
            if (entryTag != null)
            {
                _context.EntryTags.Remove(entryTag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntryTagExists(int id)
        {
            return _context.EntryTags.Any(e => e.ID == id);
        }
    }
}
