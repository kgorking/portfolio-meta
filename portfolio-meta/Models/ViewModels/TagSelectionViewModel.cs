namespace portfolio.Models.ViewModels
{
    public class TagSelectionViewModel
    {
        public List<Tag> Tags { get; set; } = new();
        public IEnumerable<int> SelectedTagIds { get; set; } = new List<int>();
    }
}