using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.DomainLayer.Models
{
    public class RoleClaimViewModel
    {
        public string RoleName { get; set; }
        public List<RoleClaim> RoleClaims { get; set; }
    }
}
