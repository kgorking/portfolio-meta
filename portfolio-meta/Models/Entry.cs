using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    public class Entry
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Extract is required.")]

        public string? Extract { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string? Content { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public List<Tag>? Tags { get; set; }
    }
}
