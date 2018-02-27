namespace Prospects.Backend.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Security.Claims;
    using System.Security.Principal;

    public class DisplayFullName
    {
    }

    public static class IdentityExtensions
    {
        public static string GetDisplayName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            var ci = identity as ClaimsIdentity;

            if (ci != null)
            {
                return ci.FindFirstValue("userFName");
            }
            return null;
        }
    }
}