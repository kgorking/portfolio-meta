using Microsoft.AspNetCore.Mvc;
using portfolio.Models.ViewModels;

namespace portfolio.ViewComponents
{
    public class EntryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(EntryWithTagsViewModel entry)
        {
            return View(entry);
        }
    }
}