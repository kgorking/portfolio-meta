using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    public class Entry
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Url is required.")]
        public string Url { get; set; } = string.Empty;

        public int[] Tags { get; set; } = [];

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
