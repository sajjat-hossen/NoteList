using NoteList.DomainLayer.Models;

namespace NoteList.ServiceLayer.Models
{
    public class RoleClaimViewModel
    {
        public string RoleName { get; set; }
        public List<RoleClaim> RoleClaims { get; set; }
    }
}
