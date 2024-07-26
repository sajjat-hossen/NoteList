using System.ComponentModel.DataAnnotations;

namespace NoteList.DomainLayer.Models
{
    public class TodoList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
