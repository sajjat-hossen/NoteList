using System.Security.Claims;

namespace NoteList.Models
{
    public static class ClaimsStore
    {
        private static readonly Dictionary<string, string[]> _claimsDictionary = new Dictionary<string, string[]>
    {
        { "Note", new[] { "Create", "Edit", "Delete", "View" } },
        { "TodoList", new[] { "Create", "Edit", "Delete", "View" } },
    };

        public static List<Claim> GetAllClaims()
        {
            var claims = new List<Claim>();

            foreach (var controller in _claimsDictionary)
            {
                string controllerName = controller.Key;
                string[] actions = controller.Value;

                foreach (var action in actions)
                {
                    string claimType = $"{action} {controllerName}";
                    claims.Add(new Claim(claimType, claimType));
                }
            }

            return claims;
        }
    }
}
