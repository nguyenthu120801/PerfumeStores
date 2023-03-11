using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static T GetUserId<T>(ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentNullException(nameof(principal));

            var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (typeof(T) == typeof(string))
            {
                return (userId is null) ? (T)Convert.ChangeType("0", typeof(T)) : (T)Convert.ChangeType(userId, typeof(T));
            }

            else if (typeof(T) == typeof(int))
            {
                return (userId is null) ? (T)Convert.ChangeType(0, typeof(T)) : (T)Convert.ChangeType(userId, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentNullException(nameof(principal));

            return principal.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}
