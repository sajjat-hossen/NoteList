using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer
{
    public static class RolesStore
    {
        public static List<string> GetAllRoles()
        {
            return new List<string>()
            {
                // Initializes a new instance of the Claim class with the specified claim type, and value.
                "SuperAdmin",
                "Admin",
                "User"
            };
        }
    }
}
