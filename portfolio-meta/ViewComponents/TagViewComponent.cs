using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio.DataContext;
using portfolio.Models.ViewModels;

namespace portfolio.ViewComponents
{
    public class TagViewComponent : ViewComponent
    {
        private readonly PortfolioContext _context;

        public TagViewComponent(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<int>? selectedTagIds = null)
        {
            var tags = await _context.Tags.ToListAsync();
            var model = new TagSelectionViewModel
            {
                Tags = tags,
                SelectedTagIds = selectedTagIds ?? new List<int>()
            };
            return View(model);
        }
    }
}