using System.ComponentModel.DataAnnotations;

namespace Models
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

        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
