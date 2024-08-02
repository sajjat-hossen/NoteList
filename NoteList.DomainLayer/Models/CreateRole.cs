using System.ComponentModel;

namespace NoteList.DomainLayer.Models
{
    public class CreateRole
    {
        [DisplayName("Role Name")]
        public string? RoleName { get; set; }
    }
}
