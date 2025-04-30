using System.ComponentModel.DataAnnotations;

namespace Models
{
    // A simple model that links a tag to an entry
    public class EntryTag
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Entry id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be a valid id")]
        public int EntryID { get; set; }

        [Required(ErrorMessage = "Tag id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be a valid id")]
        public int TagID { get; set; }
    }
}
