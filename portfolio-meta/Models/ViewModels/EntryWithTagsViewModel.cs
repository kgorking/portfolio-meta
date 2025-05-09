using portfolio.Models;

namespace portfolio.Models.ViewModels
{
    public class EntryWithTagsViewModel
    {
        public Entry Entry { get; set; } = new();
        public List<Tag> Tags { get; set; } = [];
    }
}