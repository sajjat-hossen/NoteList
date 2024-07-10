﻿using System.Security.Claims;

namespace NoteList.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> GetAllClaims()
        {
            return new List<Claim>()
            {
                // Initializes a new instance of the Claim class with the specified claim type, and value.
                new Claim("Create Role", "Create Role"),
                new Claim("Edit Role", "Edit Role"),
                new Claim("Delete Role", "Delete Role"),
                new Claim("View Role", "View Role")
            };
        }
    }
}
