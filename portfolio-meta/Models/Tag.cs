using System.ComponentModel.DataAnnotations;

namespace portfolio.Models
{
    // Describes a simple tag that can be attached to an entry,
    // to give some context regarding its contents.
    public class Tag
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}
