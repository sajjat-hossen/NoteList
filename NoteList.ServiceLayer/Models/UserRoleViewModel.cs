using NoteList.DomainLayer.Models;

namespace NoteList.ServiceLayer.Models
{
    public class UserRoleViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}
