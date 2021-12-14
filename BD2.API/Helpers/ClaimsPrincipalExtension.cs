using BD2.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TravelNow2.Api.Helpers
{
    public static class ClaimsPrincipalExtension
    {
        public static bool TryGetUserEmail(this ClaimsPrincipal user, out string email)
        {
            email = user.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase))?.Value;
            return email != null;
        }

        public static bool TryGetUserId(this ClaimsPrincipal user, out Guid? id)
        {
            id = null;
            var strId = user.FindFirstValue("Id");

            if (strId == null) return false;

            try
            {
                id = Guid.Parse(strId);
            }
            catch(Exception ex)
            {
                return false;
            }

            return id != Guid.Empty;
        }
    }
}
