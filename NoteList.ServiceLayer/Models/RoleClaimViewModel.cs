using NoteList.DomainLayer.Models;

namespace NoteList.ServiceLayer.Models
{
    public class RoleClaimViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<RoleClaim> RoleClaims { get; set; }
    }
}
