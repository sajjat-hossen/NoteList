using System.ComponentModel.DataAnnotations;

namespace NoteList.Models
{
    public class NoteViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
