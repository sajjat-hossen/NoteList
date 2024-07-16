using System.Security.Claims;

namespace NoteList.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> GetAllClaims()
        {
            return new List<Claim>()
            {
                // Initializes a new instance of the Claim class with the specified claim type, and value.
                new Claim("Create Note", "Create Note"),
                new Claim("Edit Note", "Edit Note"),
                new Claim("Delete Note", "Delete Note"),
                new Claim("View Note", "View Note"),

                new Claim("Create TodoList", "Create TodoList"),
                new Claim("Edit TodoList", "Edit TodoList"),
                new Claim("Delete TodoList", "Delete TodoList"),
                new Claim("View TodoList", "View TodoList")
            };
        }
    }
}
