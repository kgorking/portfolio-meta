using Microsoft.AspNetCore.Mvc;
using portfolio.Models;

namespace portfolio.ViewComponents
{
    public class EntryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Entry entry)
        {
            return View(entry);
        }
    }
}