using Microsoft.AspNetCore.Authorization;

namespace NoteList
{
    public static class AuthorizationPolicyHelper
    {
        public static void AddPoliciesForControllerActions(this AuthorizationOptions options, string controllerName, params string[] actions)
        {
            foreach (var action in actions)
            {
                string policyName = $"{action}{controllerName}Policy";
                string claimName = $"{action} {controllerName}";

                options.AddPolicy(policyName, policy => policy.RequireClaim(claimName));
            }
        }
    }

}
